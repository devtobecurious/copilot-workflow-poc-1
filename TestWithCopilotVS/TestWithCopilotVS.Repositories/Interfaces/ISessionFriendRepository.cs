using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des participants de session.
/// </summary>
public interface ISessionFriendRepository
{
    /// <summary>
    /// Récupère tous les participants d'une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <returns>Liste des participants de la session</returns>
    Task<IEnumerable<SessionFriend>> GetBySessionAsync(int sessionId);

    /// <summary>
    /// Récupère toutes les sessions d'un ami.
    /// </summary>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>Liste des participations de l'ami</returns>
    Task<IEnumerable<SessionFriend>> GetByFriendAsync(int friendId);

    /// <summary>
    /// Récupère une relation spécifique entre un ami et une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>La relation trouvée ou null</returns>
    Task<SessionFriend?> GetBySessionAndFriendAsync(int sessionId, int friendId);

    /// <summary>
    /// Ajoute un ami à une session.
    /// </summary>
    /// <param name="sessionFriend">La relation session-ami à créer</param>
    /// <returns>La relation créée</returns>
    Task<SessionFriend> AddAsync(SessionFriend sessionFriend);

    /// <summary>
    /// Met à jour le statut d'un ami dans une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <param name="status">Nouveau statut</param>
    /// <returns>La relation mise à jour ou null</returns>
    Task<SessionFriend?> UpdateStatusAsync(int sessionId, int friendId, FriendSessionStatus status);

    /// <summary>
    /// Retire un ami d'une session (désactive la participation).
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>True si retiré, false sinon</returns>
    Task<bool> RemoveFromSessionAsync(int sessionId, int friendId);

    /// <summary>
    /// Vérifie si un ami participe déjà à une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>True si l'ami participe, false sinon</returns>
    Task<bool> IsParticipatingAsync(int sessionId, int friendId);

    /// <summary>
    /// Compte le nombre de participants actifs dans une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <returns>Nombre de participants actifs</returns>
    Task<int> CountActiveParticipantsAsync(int sessionId);

    /// <summary>
    /// Récupère les participants par statut dans une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session</param>
    /// <param name="status">Statut recherché</param>
    /// <returns>Liste des participants avec le statut spécifié</returns>
    Task<IEnumerable<SessionFriend>> GetBySessionAndStatusAsync(int sessionId, FriendSessionStatus status);
}