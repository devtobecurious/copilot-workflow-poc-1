using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des genres.
/// </summary>
public class InMemoryGenreRepository : IGenreRepository
{
    private readonly List<Genre> _genres = new();
    private int _nextId = 1;

    /// <summary>
    /// Constructeur qui initialise quelques genres de démonstration.
    /// </summary>
    public InMemoryGenreRepository()
    {
        InitializeSampleData();
    }

    /// <summary>
    /// Récupère tous les genres.
    /// </summary>
    /// <returns>Liste de tous les genres</returns>
    public IEnumerable<Genre> GetAll()
    {
        return _genres.ToList();
    }

    /// <summary>
    /// Récupère un genre par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du genre</param>
    /// <returns>Le genre trouvé ou null</returns>
    public Genre? Get(int id)
    {
        return _genres.FirstOrDefault(g => g.Id == id);
    }

    /// <summary>
    /// Ajoute un nouveau genre.
    /// </summary>
    /// <param name="genre">Le genre à ajouter</param>
    /// <returns>Le genre ajouté avec son ID généré</returns>
    public Genre Add(Genre genre)
    {
        genre.Id = _nextId++;
        genre.CreatedAt = DateTime.UtcNow;
        _genres.Add(genre);
        return genre;
    }

    /// <summary>
    /// Met à jour un genre existant.
    /// </summary>
    /// <param name="id">Identifiant du genre à mettre à jour</param>
    /// <param name="genre">Les nouvelles données du genre</param>
    /// <returns>Le genre mis à jour ou null si non trouvé</returns>
    public Genre? Update(int id, Genre genre)
    {
        var existingGenre = Get(id);
        if (existingGenre == null)
            return null;

        existingGenre.Name = genre.Name;
        existingGenre.Description = genre.Description;
        existingGenre.IsActive = genre.IsActive;

        return existingGenre;
    }

    /// <summary>
    /// Supprime un genre.
    /// </summary>
    /// <param name="id">Identifiant du genre à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    public bool Delete(int id)
    {
        var genre = Get(id);
        if (genre == null)
            return false;

        return _genres.Remove(genre);
    }

    /// <summary>
    /// Récupère les genres actifs seulement.
    /// </summary>
    /// <returns>Liste des genres actifs</returns>
    public IEnumerable<Genre> GetActive()
    {
        return _genres.Where(g => g.IsActive);
    }

    /// <summary>
    /// Recherche des genres par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des genres correspondants</returns>
    public IEnumerable<Genre> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GetAll();

        return _genres.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Initialise quelques genres de démonstration.
    /// </summary>
    private void InitializeSampleData()
    {
        _genres.AddRange(new List<Genre>
        {
            new Genre
            {
                Id = _nextId++,
                Name = "Action",
                Description = "Jeux axés sur l'action, les combats et les réflexes",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "RPG",
                Description = "Jeux de rôle avec développement de personnages et histoire narrative",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Adventure",
                Description = "Jeux d'aventure avec exploration et résolution d'énigmes",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Strategy",
                Description = "Jeux de stratégie et de gestion",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Simulation",
                Description = "Jeux de simulation de différents aspects de la vie réelle",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Sports",
                Description = "Jeux de sport et compétition athlétique",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Racing",
                Description = "Jeux de course automobile et autres véhicules",
                IsActive = true
            },
            new Genre
            {
                Id = _nextId++,
                Name = "Fighting",
                Description = "Jeux de combat entre personnages",
                IsActive = true
            }
        });
    }
}