using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des invitations d'amis.
/// </summary>
public interface IFriendInvitationRepository
{
    /// <summary>
    /// Récupère toutes les invitations.
    /// </summary>
    /// <returns>Liste de toutes les invitations</returns>
    Task<IEnumerable<FriendInvitation>> GetAllAsync();

    /// <summary>
    /// Récupère une invitation par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de l'invitation</param>
    /// <returns>L'invitation trouvée ou null</returns>
    Task<FriendInvitation?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les invitations pour une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <returns>Liste des invitations pour la session</returns>
    Task<IEnumerable<FriendInvitation>> GetBySessionAsync(int sessionId);

    /// <summary>
    /// Récupère les invitations reçues par un ami.
    /// </summary>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>Liste des invitations reçues</returns>
    Task<IEnumerable<FriendInvitation>> GetByInvitedFriendAsync(int friendId);

    /// <summary>
    /// Récupère les invitations envoyées par un ami.
    /// </summary>
    /// <param name="invitedById">Identifiant de l'ami qui a envoyé les invitations</param>
    /// <returns>Liste des invitations envoyées</returns>
    Task<IEnumerable<FriendInvitation>> GetByInviterAsync(int invitedById);

    /// <summary>
    /// Récupère les invitations en attente pour un ami.
    /// </summary>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>Liste des invitations en attente</returns>
    Task<IEnumerable<FriendInvitation>> GetPendingInvitationsAsync(int friendId);

    /// <summary>
    /// Crée une nouvelle invitation.
    /// </summary>
    /// <param name="invitation">L'invitation à créer</param>
    /// <returns>L'invitation créée avec son ID généré</returns>
    Task<FriendInvitation> CreateAsync(FriendInvitation invitation);

    /// <summary>
    /// Met à jour une invitation existante.
    /// </summary>
    /// <param name="invitation">L'invitation à mettre à jour</param>
    /// <returns>L'invitation mise à jour ou null si non trouvée</returns>
    Task<FriendInvitation?> UpdateAsync(FriendInvitation invitation);

    /// <summary>
    /// Répond à une invitation (accepter/refuser).
    /// </summary>
    /// <param name="id">Identifiant de l'invitation</param>
    /// <param name="status">Nouveau statut (Accepted/Declined)</param>
    /// <returns>L'invitation mise à jour ou null</returns>
    Task<FriendInvitation?> RespondToInvitationAsync(int id, InvitationStatus status);

    /// <summary>
    /// Annule une invitation.
    /// </summary>
    /// <param name="id">Identifiant de l'invitation</param>
    /// <returns>True si annulée, false sinon</returns>
    Task<bool> CancelInvitationAsync(int id);

    /// <summary>
    /// Supprime une invitation.
    /// </summary>
    /// <param name="id">Identifiant de l'invitation à supprimer</param>
    /// <returns>True si supprimée, false sinon</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Marque les invitations expirées comme expirées.
    /// </summary>
    /// <returns>Nombre d'invitations expirées</returns>
    Task<int> MarkExpiredInvitationsAsync();

    /// <summary>
    /// Vérifie si un ami a déjà une invitation en attente pour une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>True si une invitation existe, false sinon</returns>
    Task<bool> HasPendingInvitationAsync(int sessionId, int friendId);
}