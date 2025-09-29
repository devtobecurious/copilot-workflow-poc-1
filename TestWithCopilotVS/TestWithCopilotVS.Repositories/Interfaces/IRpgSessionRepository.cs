using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des sessions de jeu de rôle.
/// </summary>
public interface IRpgSessionRepository
{
    /// <summary>
    /// Récupère toutes les sessions.
    /// </summary>
    /// <returns>Liste de toutes les sessions.</returns>
    Task<IEnumerable<RpgSession>> GetAllAsync();

    /// <summary>
    /// Récupère une session par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la session.</param>
    /// <returns>La session correspondante ou null si non trouvée.</returns>
    Task<RpgSession?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les sessions d'une campagne spécifique.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des sessions de la campagne.</returns>
    Task<IEnumerable<RpgSession>> GetByCampaignAsync(int campaignId);

    /// <summary>
    /// Récupère les sessions par statut.
    /// </summary>
    /// <param name="status">Statut des sessions à récupérer.</param>
    /// <param name="campaignId">Identifiant de la campagne (optionnel).</param>
    /// <returns>Liste des sessions avec ce statut.</returns>
    Task<IEnumerable<RpgSession>> GetByStatusAsync(RpgSessionStatus status, int? campaignId = null);

    /// <summary>
    /// Récupère les sessions planifiées d'une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des sessions planifiées.</returns>
    Task<IEnumerable<RpgSession>> GetPlannedSessionsAsync(int campaignId);

    /// <summary>
    /// Récupère les sessions en cours.
    /// </summary>
    /// <returns>Liste des sessions actuellement en cours.</returns>
    Task<IEnumerable<RpgSession>> GetActiveSessionsAsync();

    /// <summary>
    /// Récupère les sessions d'un maître de jeu.
    /// </summary>
    /// <param name="gameMasterId">Identifiant du maître de jeu.</param>
    /// <returns>Liste des sessions du maître de jeu.</returns>
    Task<IEnumerable<RpgSession>> GetByGameMasterAsync(int gameMasterId);

    /// <summary>
    /// Récupère les sessions dans une plage de dates.
    /// </summary>
    /// <param name="startDate">Date de début.</param>
    /// <param name="endDate">Date de fin.</param>
    /// <param name="campaignId">Identifiant de la campagne (optionnel).</param>
    /// <returns>Liste des sessions dans la plage.</returns>
    Task<IEnumerable<RpgSession>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, int? campaignId = null);

    /// <summary>
    /// Récupère la dernière session complétée d'une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>La dernière session complétée ou null.</returns>
    Task<RpgSession?> GetLastCompletedSessionAsync(int campaignId);

    /// <summary>
    /// Récupère la prochaine session planifiée d'une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>La prochaine session planifiée ou null.</returns>
    Task<RpgSession?> GetNextPlannedSessionAsync(int campaignId);

    /// <summary>
    /// Crée une nouvelle session.
    /// </summary>
    /// <param name="session">Session à créer.</param>
    /// <returns>La session créée avec son identifiant.</returns>
    Task<RpgSession> CreateAsync(RpgSession session);

    /// <summary>
    /// Met à jour une session existante.
    /// </summary>
    /// <param name="session">Session avec les modifications.</param>
    /// <returns>La session mise à jour ou null si non trouvée.</returns>
    Task<RpgSession?> UpdateAsync(RpgSession session);

    /// <summary>
    /// Supprime une session par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la session à supprimer.</param>
    /// <returns>True si supprimée, false si non trouvée.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Vérifie si une session existe.
    /// </summary>
    /// <param name="id">Identifiant de la session.</param>
    /// <returns>True si la session existe.</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Met à jour le statut d'une session.
    /// </summary>
    /// <param name="sessionId">Identifiant de la session.</param>
    /// <param name="status">Nouveau statut.</param>
    /// <returns>True si mis à jour avec succès.</returns>
    Task<bool> UpdateStatusAsync(int sessionId, RpgSessionStatus status);

    /// <summary>
    /// Démarre une session (met le statut à InProgress et enregistre l'heure de début).
    /// </summary>
    /// <param name="sessionId">Identifiant de la session.</param>
    /// <returns>True si démarrée avec succès.</returns>
    Task<bool> StartSessionAsync(int sessionId);

    /// <summary>
    /// Termine une session (met le statut à Completed et enregistre l'heure de fin).
    /// </summary>
    /// <param name="sessionId">Identifiant de la session.</param>
    /// <param name="summary">Résumé de la session.</param>
    /// <returns>True si terminée avec succès.</returns>
    Task<bool> CompleteSessionAsync(int sessionId, string? summary = null);
}