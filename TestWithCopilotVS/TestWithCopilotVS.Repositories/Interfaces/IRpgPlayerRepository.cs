using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des joueurs de jeu de rôle.
/// </summary>
public interface IRpgPlayerRepository
{
    /// <summary>
    /// Récupère tous les joueurs.
    /// </summary>
    /// <returns>Liste de tous les joueurs.</returns>
    Task<IEnumerable<RpgPlayer>> GetAllAsync();

    /// <summary>
    /// Récupère un joueur par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du joueur.</param>
    /// <returns>Le joueur correspondant ou null si non trouvé.</returns>
    Task<RpgPlayer?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère les joueurs d'une campagne spécifique.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des joueurs de la campagne.</returns>
    Task<IEnumerable<RpgPlayer>> GetByCampaignAsync(int campaignId);

    /// <summary>
    /// Récupère les joueurs actifs d'une campagne.
    /// </summary>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Liste des joueurs actifs.</returns>
    Task<IEnumerable<RpgPlayer>> GetActiveByCampaignAsync(int campaignId);

    /// <summary>
    /// Récupère un joueur par son nom d'utilisateur et campagne.
    /// </summary>
    /// <param name="username">Nom d'utilisateur.</param>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Le joueur correspondant ou null.</returns>
    Task<RpgPlayer?> GetByUsernameAndCampaignAsync(string username, int campaignId);

    /// <summary>
    /// Récupère un joueur par son email et campagne.
    /// </summary>
    /// <param name="email">Email du joueur.</param>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <returns>Le joueur correspondant ou null.</returns>
    Task<RpgPlayer?> GetByEmailAndCampaignAsync(string email, int campaignId);

    /// <summary>
    /// Récupère les maîtres de jeu.
    /// </summary>
    /// <returns>Liste des maîtres de jeu.</returns>
    Task<IEnumerable<RpgPlayer>> GetGameMastersAsync();

    /// <summary>
    /// Ajoute un nouveau joueur.
    /// </summary>
    /// <param name="player">Joueur à ajouter.</param>
    /// <returns>Le joueur créé avec son identifiant.</returns>
    Task<RpgPlayer> CreateAsync(RpgPlayer player);

    /// <summary>
    /// Met à jour un joueur existant.
    /// </summary>
    /// <param name="player">Joueur avec les modifications.</param>
    /// <returns>Le joueur mis à jour ou null si non trouvé.</returns>
    Task<RpgPlayer?> UpdateAsync(RpgPlayer player);

    /// <summary>
    /// Supprime un joueur par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du joueur à supprimer.</param>
    /// <returns>True si supprimé, false si non trouvé.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Vérifie si un joueur existe.
    /// </summary>
    /// <param name="id">Identifiant du joueur.</param>
    /// <returns>True si le joueur existe.</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Vérifie si un nom d'utilisateur est déjà pris dans une campagne.
    /// </summary>
    /// <param name="username">Nom d'utilisateur à vérifier.</param>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <param name="excludePlayerId">ID du joueur à exclure de la vérification (pour les mises à jour).</param>
    /// <returns>True si le nom est déjà pris.</returns>
    Task<bool> IsUsernameInUseAsync(string username, int campaignId, int? excludePlayerId = null);

    /// <summary>
    /// Vérifie si un email est déjà utilisé dans une campagne.
    /// </summary>
    /// <param name="email">Email à vérifier.</param>
    /// <param name="campaignId">Identifiant de la campagne.</param>
    /// <param name="excludePlayerId">ID du joueur à exclure de la vérification.</param>
    /// <returns>True si l'email est déjà utilisé.</returns>
    Task<bool> IsEmailInUseAsync(string email, int campaignId, int? excludePlayerId = null);
}