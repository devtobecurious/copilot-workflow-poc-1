using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des sessions de jeu.
/// </summary>
public interface IGameSessionRepository
{
    /// <summary>
    /// Récupère toutes les sessions de jeu.
    /// </summary>
    /// <returns>Liste de toutes les sessions</returns>
    Task<IEnumerable<GameSession>> GetAllAsync();

    /// <summary>
    /// Récupère une session par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la session</param>
    /// <returns>La session trouvée ou null</returns>
    Task<GameSession?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les sessions actives.
    /// </summary>
    /// <returns>Liste des sessions actives</returns>
    Task<IEnumerable<GameSession>> GetActiveSessionsAsync();

    /// <summary>
    /// Récupère les sessions créées par un ami spécifique.
    /// </summary>
    /// <param name="creatorId">Identifiant du créateur</param>
    /// <returns>Liste des sessions créées par l'ami</returns>
    Task<IEnumerable<GameSession>> GetByCreatorAsync(int creatorId);

    /// <summary>
    /// Récupère les sessions où un ami participe.
    /// </summary>
    /// <param name="friendId">Identifiant de l'ami</param>
    /// <returns>Liste des sessions où l'ami participe</returns>
    Task<IEnumerable<GameSession>> GetByParticipantAsync(int friendId);

    /// <summary>
    /// Crée une nouvelle session de jeu.
    /// </summary>
    /// <param name="session">La session à créer</param>
    /// <returns>La session créée avec son ID généré</returns>
    Task<GameSession> CreateAsync(GameSession session);

    /// <summary>
    /// Met à jour une session existante.
    /// </summary>
    /// <param name="session">La session à mettre à jour</param>
    /// <returns>La session mise à jour ou null si non trouvée</returns>
    Task<GameSession?> UpdateAsync(GameSession session);

    /// <summary>
    /// Supprime une session.
    /// </summary>
    /// <param name="id">Identifiant de la session à supprimer</param>
    /// <returns>True si supprimée, false sinon</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Termine une session active.
    /// </summary>
    /// <param name="id">Identifiant de la session</param>
    /// <returns>True si terminée, false sinon</returns>
    Task<bool> EndSessionAsync(int id);
}