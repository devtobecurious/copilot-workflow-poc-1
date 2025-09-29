using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Extensions pour configurer les endpoints de gestion des amis dans les sessions.
/// </summary>
public static class SessionFriendEndpoints
{
    /// <summary>
    /// Configure les endpoints pour la gestion des amis dans les sessions.
    /// </summary>
    /// <param name="app">L'application web</param>
    /// <returns>L'application web avec les endpoints configurés</returns>
    public static WebApplication MapSessionFriendEndpoints(this WebApplication app)
    {
        var sessionFriendGroup = app.MapGroup("/api/sessions/{sessionId:int}/friends")
            .WithTags("Session Friends")
            .WithDescription("Gestion des amis dans les sessions");

        // GET /api/sessions/{sessionId}/friends - Lister tous les amis d'une session
        sessionFriendGroup.MapGet("", async (int sessionId, 
            ISessionFriendRepository sessionFriendRepository,
            IFriendRepository friendRepository,
            IGameSessionRepository sessionRepository) =>
        {
            // Vérifier que la session existe
            var session = await sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound($"Session {sessionId} non trouvée");
            }

            var sessionFriends = await sessionFriendRepository.GetBySessionAsync(sessionId);
            
            // Enrichir avec les informations des amis
            var friendsWithDetails = sessionFriends.Select(sf =>
            {
                var friend = friendRepository.Get(sf.FriendId);
                return new SessionFriendResponseDto
                {
                    Friend = friend!,
                    Status = sf.Status,
                    JoinedAt = sf.JoinedAt,
                    IsActive = sf.IsActive
                };
            }).Where(f => f.Friend != null);

            return Results.Ok(friendsWithDetails);
        })
        .WithName("GetSessionFriends")
        .WithSummary("Lister les amis d'une session")
        .WithDescription("Retourne tous les amis participant à une session avec leur statut");

        // POST /api/sessions/{sessionId}/friends - Ajouter un ami à une session
        sessionFriendGroup.MapPost("", async (int sessionId, 
            [FromBody] AddFriendToSessionDto addFriendDto,
            ISessionFriendRepository sessionFriendRepository,
            IFriendRepository friendRepository,
            IGameSessionRepository sessionRepository,
            IFriendInvitationRepository invitationRepository) =>
        {
            // Vérifier que la session existe et est active
            var session = await sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound($"Session {sessionId} non trouvée");
            }

            if (!session.IsActive)
            {
                return Results.BadRequest("Impossible d'ajouter des amis à une session terminée");
            }

            // Vérifier que l'ami existe
            var friend = friendRepository.Get(addFriendDto.FriendId);
            if (friend == null)
            {
                return Results.NotFound($"Ami {addFriendDto.FriendId} non trouvé");
            }

            // Vérifier que l'ami n'est pas déjà dans la session
            var existingParticipation = await sessionFriendRepository.IsParticipatingAsync(sessionId, addFriendDto.FriendId);
            if (existingParticipation)
            {
                return Results.Conflict($"L'ami {addFriendDto.FriendId} participe déjà à cette session");
            }

            // Vérifier qu'il n'y a pas déjà une invitation en attente
            var hasPendingInvitation = await invitationRepository.HasPendingInvitationAsync(sessionId, addFriendDto.FriendId);
            if (hasPendingInvitation)
            {
                return Results.Conflict($"Une invitation est déjà en attente pour l'ami {addFriendDto.FriendId}");
            }

            // Ajouter l'ami à la session
            var sessionFriend = new SessionFriend
            {
                SessionId = sessionId,
                FriendId = addFriendDto.FriendId,
                Status = addFriendDto.Status
            };

            var addedSessionFriend = await sessionFriendRepository.AddAsync(sessionFriend);

            // Créer l'invitation si nécessaire (pour traçabilité)
            if (addFriendDto.Status == FriendSessionStatus.Secondary)
            {
                var invitation = new FriendInvitation
                {
                    SessionId = sessionId,
                    FriendId = addFriendDto.FriendId,
                    InvitedById = session.CreatorId,
                    Message = addFriendDto.Message,
                    Status = InvitationStatus.Accepted
                };
                await invitationRepository.CreateAsync(invitation);
            }

            return Results.Created($"/api/sessions/{sessionId}/friends/{addFriendDto.FriendId}", 
                new SessionFriendResponseDto
                {
                    Friend = friend,
                    Status = addedSessionFriend.Status,
                    JoinedAt = addedSessionFriend.JoinedAt,
                    IsActive = addedSessionFriend.IsActive
                });
        })
        .WithName("AddFriendToSession")
        .WithSummary("Ajouter un ami à une session")
        .WithDescription("Ajoute un ami secondaire à une session active");

        // PUT /api/sessions/{sessionId}/friends/{friendId}/status - Modifier le statut d'un ami
        sessionFriendGroup.MapPut("{friendId:int}/status", async (int sessionId, int friendId,
            [FromBody] UpdateFriendStatusDto statusDto,
            ISessionFriendRepository sessionFriendRepository,
            IGameSessionRepository sessionRepository) =>
        {
            // Vérifier que la session existe
            var session = await sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound($"Session {sessionId} non trouvée");
            }

            // Mettre à jour le statut
            var updatedSessionFriend = await sessionFriendRepository.UpdateStatusAsync(sessionId, friendId, statusDto.Status);
            if (updatedSessionFriend == null)
            {
                return Results.NotFound($"Participation de l'ami {friendId} dans la session {sessionId} non trouvée");
            }

            return Results.Ok($"Statut de l'ami {friendId} mis à jour vers {statusDto.Status}");
        })
        .WithName("UpdateFriendStatus")
        .WithSummary("Modifier le statut d'un ami")
        .WithDescription("Change le statut d'un ami dans une session");

        // DELETE /api/sessions/{sessionId}/friends/{friendId} - Retirer un ami de la session
        sessionFriendGroup.MapDelete("{friendId:int}", async (int sessionId, int friendId,
            ISessionFriendRepository sessionFriendRepository,
            IGameSessionRepository sessionRepository) =>
        {
            // Vérifier que la session existe
            var session = await sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound($"Session {sessionId} non trouvée");
            }

            // Ne pas permettre de retirer le créateur
            if (friendId == session.CreatorId)
            {
                return Results.BadRequest("Impossible de retirer le créateur de la session");
            }

            // Retirer l'ami de la session
            var result = await sessionFriendRepository.RemoveFromSessionAsync(sessionId, friendId);
            if (!result)
            {
                return Results.NotFound($"Ami {friendId} non trouvé dans la session {sessionId}");
            }

            return Results.Ok($"Ami {friendId} retiré de la session {sessionId}");
        })
        .WithName("RemoveFriendFromSession")
        .WithSummary("Retirer un ami de la session")
        .WithDescription("Retire un ami d'une session (ne peut pas retirer le créateur)");

        // GET /api/sessions/{sessionId}/friends/count - Compter les participants
        sessionFriendGroup.MapGet("count", async (int sessionId,
            ISessionFriendRepository sessionFriendRepository,
            IGameSessionRepository sessionRepository) =>
        {
            // Vérifier que la session existe
            var session = await sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound($"Session {sessionId} non trouvée");
            }

            var count = await sessionFriendRepository.CountActiveParticipantsAsync(sessionId);
            return Results.Ok(new { SessionId = sessionId, ActiveParticipants = count });
        })
        .WithName("CountSessionParticipants")
        .WithSummary("Compter les participants")
        .WithDescription("Retourne le nombre de participants actifs dans une session");

        return app;
    }
}