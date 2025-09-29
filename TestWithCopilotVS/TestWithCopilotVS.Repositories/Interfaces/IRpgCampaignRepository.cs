using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des campagnes de jeu de rôle.
/// </summary>
public interface IRpgCampaignRepository
{
    /// <summary>
    /// Récupère toutes les campagnes.
    /// </summary>
    /// <returns>Liste de toutes les campagnes.</returns>
    Task<IEnumerable<RpgCampaign>> GetAllAsync();

    /// <summary>
    /// Récupère une campagne par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la campagne.</param>
    /// <returns>La campagne correspondante ou null si non trouvée.</returns>
    Task<RpgCampaign?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les campagnes actives.
    /// </summary>
    /// <returns>Liste des campagnes actives.</returns>
    Task<IEnumerable<RpgCampaign>> GetActiveCampaignsAsync();

    /// <summary>
    /// Récupère les campagnes d'un maître de jeu spécifique.
    /// </summary>
    /// <param name="gameMasterId">Identifiant du maître de jeu.</param>
    /// <returns>Liste des campagnes du maître de jeu.</returns>
    Task<IEnumerable<RpgCampaign>> GetByGameMasterAsync(int gameMasterId);

    /// <summary>
    /// Récupère les campagnes par système de jeu.
    /// </summary>
    /// <param name="gameSystem">Système de jeu.</param>
    /// <returns>Liste des campagnes utilisant ce système.</returns>
    Task<IEnumerable<RpgCampaign>> GetByGameSystemAsync(string gameSystem);

    /// <summary>
    /// Crée une nouvelle campagne.
    /// </summary>
    /// <param name="campaign">Campagne à créer.</param>
    /// <returns>La campagne créée avec son identifiant.</returns>
    Task<RpgCampaign> CreateAsync(RpgCampaign campaign);

    /// <summary>
    /// Met à jour une campagne existante.
    /// </summary>
    /// <param name="campaign">Campagne avec les modifications.</param>
    /// <returns>La campagne mise à jour ou null si non trouvée.</returns>
    Task<RpgCampaign?> UpdateAsync(RpgCampaign campaign);

    /// <summary>
    /// Supprime une campagne par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la campagne à supprimer.</param>
    /// <returns>True si supprimée, false si non trouvée.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Vérifie si une campagne existe.
    /// </summary>
    /// <param name="id">Identifiant de la campagne.</param>
    /// <returns>True si la campagne existe.</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Récupère le nombre de joueurs actifs dans une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Nombre de joueurs actifs.</returns>
    Task<int> GetActivePlayerCountAsync(int campaignId);

    /// <summary>
    /// Vérifie si un joueur peut rejoindre une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>True si la campagne accepte de nouveaux joueurs.</returns>
    Task<bool> CanAcceptNewPlayerAsync(int campaignId);
}