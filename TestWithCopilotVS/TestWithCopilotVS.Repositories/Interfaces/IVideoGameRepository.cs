using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des jeux vidéo.
/// </summary>
public interface IVideoGameRepository
{
    /// <summary>
    /// Récupère tous les jeux vidéo.
    /// </summary>
    /// <returns>Liste de tous les jeux vidéo</returns>
    IEnumerable<VideoGame> GetAll();

    /// <summary>
    /// Récupère un jeu vidéo par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo</param>
    /// <returns>Le jeu vidéo trouvé ou null</returns>
    VideoGame? Get(int id);

    /// <summary>
    /// Ajoute un nouveau jeu vidéo.
    /// </summary>
    /// <param name="videoGame">Le jeu vidéo à ajouter</param>
    /// <returns>Le jeu vidéo ajouté avec son ID généré</returns>
    VideoGame Add(VideoGame videoGame);

    /// <summary>
    /// Met à jour un jeu vidéo existant.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo à mettre à jour</param>
    /// <param name="videoGame">Les nouvelles données du jeu vidéo</param>
    /// <returns>Le jeu vidéo mis à jour ou null si non trouvé</returns>
    VideoGame? Update(int id, VideoGame videoGame);

    /// <summary>
    /// Supprime un jeu vidéo.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    bool Delete(int id);

    /// <summary>
    /// Recherche des jeux vidéo par titre.
    /// </summary>
    /// <param name="title">Titre ou partie du titre à rechercher</param>
    /// <returns>Liste des jeux vidéo correspondants</returns>
    IEnumerable<VideoGame> SearchByTitle(string title);

    /// <summary>
    /// Récupère les jeux vidéo par genre.
    /// </summary>
    /// <param name="genreId">Identifiant du genre</param>
    /// <returns>Liste des jeux vidéo du genre spécifié</returns>
    IEnumerable<VideoGame> GetByGenre(int genreId);

    /// <summary>
    /// Récupère les jeux vidéo par plateforme.
    /// </summary>
    /// <param name="platformId">Identifiant de la plateforme</param>
    /// <returns>Liste des jeux vidéo de la plateforme spécifiée</returns>
    IEnumerable<VideoGame> GetByPlatform(int platformId);

    /// <summary>
    /// Récupère les jeux vidéo par éditeur.
    /// </summary>
    /// <param name="publisherId">Identifiant de l'éditeur</param>
    /// <returns>Liste des jeux vidéo de l'éditeur spécifié</returns>
    IEnumerable<VideoGame> GetByPublisher(int publisherId);

    /// <summary>
    /// Récupère les jeux vidéo disponibles seulement.
    /// </summary>
    /// <returns>Liste des jeux vidéo disponibles</returns>
    IEnumerable<VideoGame> GetAvailable();

    /// <summary>
    /// Récupère les jeux vidéo dans une fourchette de prix.
    /// </summary>
    /// <param name="minPrice">Prix minimum</param>
    /// <param name="maxPrice">Prix maximum</param>
    /// <returns>Liste des jeux vidéo dans la fourchette de prix</returns>
    IEnumerable<VideoGame> GetByPriceRange(decimal minPrice, decimal maxPrice);
}