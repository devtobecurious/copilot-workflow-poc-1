using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Extensions pour configurer les endpoints des sessions de jeu.
/// </summary>
public static class SessionEndpoints
{
    /// <summary>
    /// Configure les endpoints pour la gestion des sessions de jeu.
    /// </summary>
    /// <param name="app">L'application web</param>
    /// <returns>L'application web avec les endpoints configurés</returns>
    public static WebApplication MapSessionEndpoints(this WebApplication app)
    {
        var sessionGroup = app.MapGroup("/api/sessions")
            .WithTags("Sessions")
            .WithDescription("Gestion des sessions de jeu");

        // GET /api/sessions - Récupérer toutes les sessions
        sessionGroup.MapGet("", async (IGameSessionRepository sessionRepository) =>
        {
            var sessions = await sessionRepository.GetAllAsync();
            return Results.Ok(sessions);
        })
        .WithName("GetAllSessions")
        .WithSummary("Récupérer toutes les sessions")
        .WithDescription("Retourne la liste de toutes les sessions de jeu");

        // GET /api/sessions/{id} - Récupérer une session par ID
        sessionGroup.MapGet("{id:int}", async (int id, IGameSessionRepository sessionRepository) =>
        {
            var session = await sessionRepository.GetByIdAsync(id);
            return session != null ? Results.Ok(session) : Results.NotFound($"Session avec l'ID {id} non trouvée");
        })
        .WithName("GetSessionById")
        .WithSummary("Récupérer une session par son ID")
        .WithDescription("Retourne les détails d'une session spécifique");

        // GET /api/sessions/active - Récupérer les sessions actives
        sessionGroup.MapGet("active", async (IGameSessionRepository sessionRepository) =>
        {
            var activeSessions = await sessionRepository.GetActiveSessionsAsync();
            return Results.Ok(activeSessions);
        })
        .WithName("GetActiveSessions")
        .WithSummary("Récupérer les sessions actives")
        .WithDescription("Retourne toutes les sessions actuellement actives");

        // GET /api/sessions/creator/{creatorId} - Sessions d'un créateur
        sessionGroup.MapGet("creator/{creatorId:int}", async (int creatorId, IGameSessionRepository sessionRepository) =>
        {
            var creatorSessions = await sessionRepository.GetByCreatorAsync(creatorId);
            return Results.Ok(creatorSessions);
        })
        .WithName("GetSessionsByCreator")
        .WithSummary("Récupérer les sessions d'un créateur")
        .WithDescription("Retourne toutes les sessions créées par un ami spécifique");

        // POST /api/sessions - Créer une nouvelle session
        sessionGroup.MapPost("", async ([FromBody] CreateGameSessionDto sessionDto, 
            IGameSessionRepository sessionRepository,
            ISessionFriendRepository sessionFriendRepository,
            IFriendRepository friendRepository) =>
        {
            // Valider que le créateur existe
            var creator = friendRepository.Get(sessionDto.CreatorId);
            if (creator == null)
            {
                return Results.BadRequest($"Ami avec l'ID {sessionDto.CreatorId} non trouvé");
            }

            // Créer la session
            var newSession = new GameSession
            {
                Name = sessionDto.Name,
                CreatorId = sessionDto.CreatorId,
                IsActive = true
            };

            var createdSession = await sessionRepository.CreateAsync(newSession);

            // Ajouter le créateur comme participant primaire
            var creatorParticipation = new SessionFriend
            {
                SessionId = createdSession.Id,
                FriendId = sessionDto.CreatorId,
                Status = FriendSessionStatus.Primary
            };
            await sessionFriendRepository.AddAsync(creatorParticipation);

            // Ajouter les amis initiaux comme participants primaires
            foreach (var friendId in sessionDto.InitialFriendIds)
            {
                var friend = friendRepository.Get(friendId);
                if (friend != null)
                {
                    var friendParticipation = new SessionFriend
                    {
                        SessionId = createdSession.Id,
                        FriendId = friendId,
                        Status = FriendSessionStatus.Primary
                    };
                    await sessionFriendRepository.AddAsync(friendParticipation);
                }
            }

            return Results.Created($"/api/sessions/{createdSession.Id}", createdSession);
        })
        .WithName("CreateSession")
        .WithSummary("Créer une nouvelle session")
        .WithDescription("Crée une nouvelle session de jeu avec les participants initiaux");

        // PUT /api/sessions/{id}/end - Terminer une session
        sessionGroup.MapPut("{id:int}/end", async (int id, IGameSessionRepository sessionRepository) =>
        {
            var result = await sessionRepository.EndSessionAsync(id);
            return result ? Results.Ok($"Session {id} terminée avec succès") 
                         : Results.NotFound($"Session {id} non trouvée ou déjà terminée");
        })
        .WithName("EndSession")
        .WithSummary("Terminer une session")
        .WithDescription("Met fin à une session active");

        // DELETE /api/sessions/{id} - Supprimer une session
        sessionGroup.MapDelete("{id:int}", async (int id, IGameSessionRepository sessionRepository) =>
        {
            var result = await sessionRepository.DeleteAsync(id);
            return result ? Results.Ok($"Session {id} supprimée avec succès")
                         : Results.NotFound($"Session {id} non trouvée");
        })
        .WithName("DeleteSession")
        .WithSummary("Supprimer une session")
        .WithDescription("Supprime définitivement une session");

        return app;
    }
}