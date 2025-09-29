using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des personnages de jeu de rôle.
/// </summary>
public interface IRpgCharacterRepository
{
    /// <summary>
    /// Récupère tous les personnages.
    /// </summary>
    /// <returns>Liste de tous les personnages.</returns>
    Task<IEnumerable<RpgCharacter>> GetAllAsync();

    /// <summary>
    /// Récupère un personnage par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du personnage.</param>
    /// <returns>Le personnage correspondant ou null si non trouvé.</returns>
    Task<RpgCharacter?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les personnages d'un joueur spécifique.
    /// </summary>
    /// <param name="playerId">Identifiant du joueur.</param>
    /// <returns>Liste des personnages du joueur.</returns>
    Task<IEnumerable<RpgCharacter>> GetByPlayerAsync(int playerId);

    /// <summary>
    /// Récupère les personnages d'une campagne spécifique.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des personnages de la campagne.</returns>
    Task<IEnumerable<RpgCharacter>> GetByCampaignAsync(int campaignId);

    /// <summary>
    /// Récupère les personnages actifs d'une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des personnages actifs.</returns>
    Task<IEnumerable<RpgCharacter>> GetActiveByCampaignAsync(int campaignId);

    /// <summary>
    /// Récupère le personnage principal d'un joueur dans une campagne.
    /// </summary>
    /// <param name="playerId">Identifiant du joueur.</param>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Le personnage principal ou null.</returns>
    Task<RpgCharacter?> GetMainCharacterAsync(int playerId, int campaignId);

    /// <summary>
    /// Récupère les personnages par classe.
    /// </summary>
    /// <param name="characterClass">Classe du personnage.</param>
    /// <param name="campaignId">Identifiant de la campagne (optionnel).</param>
    /// <returns>Liste des personnages de cette classe.</returns>
    Task<IEnumerable<RpgCharacter>> GetByClassAsync(string characterClass, int? campaignId = null);

    /// <summary>
    /// Récupère les personnages par niveau.
    /// </summary>
    /// <param name="minLevel">Niveau minimum.</param>
    /// <param name="maxLevel">Niveau maximum.</param>
    /// <param name="campaignId">Identifiant de la campagne (optionnel).</param>
    /// <returns>Liste des personnages dans cette plage de niveaux.</returns>
    Task<IEnumerable<RpgCharacter>> GetByLevelRangeAsync(int minLevel, int maxLevel, int? campaignId = null);

    /// <summary>
    /// Crée un nouveau personnage.
    /// </summary>
    /// <param name="character">Personnage à créer.</param>
    /// <returns>Le personnage créé avec son identifiant.</returns>
    Task<RpgCharacter> CreateAsync(RpgCharacter character);

    /// <summary>
    /// Met à jour un personnage existant.
    /// </summary>
    /// <param name="character">Personnage avec les modifications.</param>
    /// <returns>Le personnage mis à jour ou null si non trouvé.</returns>
    Task<RpgCharacter?> UpdateAsync(RpgCharacter character);

    /// <summary>
    /// Supprime un personnage par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du personnage à supprimer.</param>
    /// <returns>True si supprimé, false si non trouvé.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Vérifie si un personnage existe.
    /// </summary>
    /// <param name="id">Identifiant du personnage.</param>
    /// <returns>True si le personnage existe.</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Vérifie si un personnage appartient à un joueur spécifique.
    /// </summary>
    /// <param name="characterId">Identifiant du personnage.</param>
    /// <param name="playerId">Identifiant du joueur.</param>
    /// <returns>True si le personnage appartient au joueur.</returns>
    Task<bool> BelongsToPlayerAsync(int characterId, int playerId);

    /// <summary>
    /// Met à jour les points de vie d'un personnage.
    /// </summary>
    /// <param name="characterId">Identifiant du personnage.</param>
    /// <param name="currentHitPoints">Nouveaux points de vie actuels.</param>
    /// <returns>True si mis à jour avec succès.</returns>
    Task<bool> UpdateHitPointsAsync(int characterId, int currentHitPoints);

    /// <summary>
    /// Ajoute de l'expérience à un personnage.
    /// </summary>
    /// <param name="characterId">Identifiant du personnage.</param>
    /// <param name="experiencePoints">Points d'expérience à ajouter.</param>
    /// <returns>True si mis à jour avec succès.</returns>
    Task<bool> AddExperienceAsync(int characterId, int experiencePoints);
}