using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des éditeurs de jeux vidéo.
/// </summary>
public interface IPublisherRepository
{
    /// <summary>
    /// Récupère tous les éditeurs.
    /// </summary>
    /// <returns>Liste de tous les éditeurs</returns>
    IEnumerable<Publisher> GetAll();

    /// <summary>
    /// Récupère un éditeur par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur</param>
    /// <returns>L'éditeur trouvé ou null</returns>
    Publisher? Get(int id);

    /// <summary>
    /// Ajoute un nouvel éditeur.
    /// </summary>
    /// <param name="publisher">L'éditeur à ajouter</param>
    /// <returns>L'éditeur ajouté avec son ID généré</returns>
    Publisher Add(Publisher publisher);

    /// <summary>
    /// Met à jour un éditeur existant.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur à mettre à jour</param>
    /// <param name="publisher">Les nouvelles données de l'éditeur</param>
    /// <returns>L'éditeur mis à jour ou null si non trouvé</returns>
    Publisher? Update(int id, Publisher publisher);

    /// <summary>
    /// Supprime un éditeur.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    bool Delete(int id);

    /// <summary>
    /// Récupère les éditeurs actifs seulement.
    /// </summary>
    /// <returns>Liste des éditeurs actifs</returns>
    IEnumerable<Publisher> GetActive();

    /// <summary>
    /// Recherche des éditeurs par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des éditeurs correspondants</returns>
    IEnumerable<Publisher> SearchByName(string name);

    /// <summary>
    /// Récupère les éditeurs par pays.
    /// </summary>
    /// <param name="country">Nom du pays</param>
    /// <returns>Liste des éditeurs du pays spécifié</returns>
    IEnumerable<Publisher> GetByCountry(string country);
}