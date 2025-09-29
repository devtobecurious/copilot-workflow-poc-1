using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des genres de jeux vidéo.
/// </summary>
public interface IGenreRepository
{
    /// <summary>
    /// Récupère tous les genres.
    /// </summary>
    /// <returns>Liste de tous les genres</returns>
    IEnumerable<Genre> GetAll();

    /// <summary>
    /// Récupère un genre par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du genre</param>
    /// <returns>Le genre trouvé ou null</returns>
    Genre? Get(int id);

    /// <summary>
    /// Ajoute un nouveau genre.
    /// </summary>
    /// <param name="genre">Le genre à ajouter</param>
    /// <returns>Le genre ajouté avec son ID généré</returns>
    Genre Add(Genre genre);

    /// <summary>
    /// Met à jour un genre existant.
    /// </summary>
    /// <param name="id">Identifiant du genre à mettre à jour</param>
    /// <param name="genre">Les nouvelles données du genre</param>
    /// <returns>Le genre mis à jour ou null si non trouvé</returns>
    Genre? Update(int id, Genre genre);

    /// <summary>
    /// Supprime un genre.
    /// </summary>
    /// <param name="id">Identifiant du genre à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    bool Delete(int id);

    /// <summary>
    /// Récupère les genres actifs seulement.
    /// </summary>
    /// <returns>Liste des genres actifs</returns>
    IEnumerable<Genre> GetActive();

    /// <summary>
    /// Recherche des genres par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des genres correspondants</returns>
    IEnumerable<Genre> SearchByName(string name);
}