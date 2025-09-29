using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des statistiques de jeu.
/// </summary>
public interface IStatistiqueRepository
{
    /// <summary>
    /// Récupère toutes les statistiques.
    /// </summary>
    IEnumerable<Statistique> GetAll();

    /// <summary>
    /// Ajoute une nouvelle statistique.
    /// </summary>
    /// <param name="stat">La statistique à ajouter</param>
    /// <returns>La statistique ajoutée avec son ID généré</returns>
    Statistique Add(Statistique stat);

    /// <summary>
    /// Récupère une statistique par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la statistique</param>
    /// <returns>La statistique trouvée ou null</returns>
    Statistique? GetById(int id);

    /// <summary>
    /// Supprime une statistique par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la statistique à supprimer</param>
    /// <returns>True si supprimée, false sinon</returns>
    bool Delete(int id);
}