using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des plateformes de jeux vidéo.
/// </summary>
public interface IPlatformRepository
{
    /// <summary>
    /// Récupère toutes les plateformes.
    /// </summary>
    /// <returns>Liste de toutes les plateformes</returns>
    IEnumerable<Platform> GetAll();

    /// <summary>
    /// Récupère une plateforme par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme</param>
    /// <returns>La plateforme trouvée ou null</returns>
    Platform? Get(int id);

    /// <summary>
    /// Ajoute une nouvelle plateforme.
    /// </summary>
    /// <param name="platform">La plateforme à ajouter</param>
    /// <returns>La plateforme ajoutée avec son ID généré</returns>
    Platform Add(Platform platform);

    /// <summary>
    /// Met à jour une plateforme existante.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme à mettre à jour</param>
    /// <param name="platform">Les nouvelles données de la plateforme</param>
    /// <returns>La plateforme mise à jour ou null si non trouvée</returns>
    Platform? Update(int id, Platform platform);

    /// <summary>
    /// Supprime une plateforme.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme à supprimer</param>
    /// <returns>True si supprimée, false sinon</returns>
    bool Delete(int id);

    /// <summary>
    /// Récupère les plateformes actives seulement.
    /// </summary>
    /// <returns>Liste des plateformes actives</returns>
    IEnumerable<Platform> GetActive();

    /// <summary>
    /// Récupère les plateformes par type.
    /// </summary>
    /// <param name="type">Type de plateforme</param>
    /// <returns>Liste des plateformes du type spécifié</returns>
    IEnumerable<Platform> GetByType(PlatformType type);

    /// <summary>
    /// Recherche des plateformes par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des plateformes correspondantes</returns>
    IEnumerable<Platform> SearchByName(string name);
}