using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des plateformes.
/// </summary>
public class InMemoryPlatformRepository : IPlatformRepository
{
    private readonly List<Platform> _platforms = new();
    private int _nextId = 1;

    /// <summary>
    /// Constructeur qui initialise quelques plateformes de démonstration.
    /// </summary>
    public InMemoryPlatformRepository()
    {
        InitializeSampleData();
    }

    /// <summary>
    /// Récupère toutes les plateformes.
    /// </summary>
    /// <returns>Liste de toutes les plateformes</returns>
    public IEnumerable<Platform> GetAll()
    {
        return _platforms.ToList();
    }

    /// <summary>
    /// Récupère une plateforme par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme</param>
    /// <returns>La plateforme trouvée ou null</returns>
    public Platform? Get(int id)
    {
        return _platforms.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Ajoute une nouvelle plateforme.
    /// </summary>
    /// <param name="platform">La plateforme à ajouter</param>
    /// <returns>La plateforme ajoutée avec son ID généré</returns>
    public Platform Add(Platform platform)
    {
        platform.Id = _nextId++;
        platform.CreatedAt = DateTime.UtcNow;
        _platforms.Add(platform);
        return platform;
    }

    /// <summary>
    /// Met à jour une plateforme existante.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme à mettre à jour</param>
    /// <param name="platform">Les nouvelles données de la plateforme</param>
    /// <returns>La plateforme mise à jour ou null si non trouvée</returns>
    public Platform? Update(int id, Platform platform)
    {
        var existingPlatform = Get(id);
        if (existingPlatform == null)
            return null;

        existingPlatform.Name = platform.Name;
        existingPlatform.ShortName = platform.ShortName;
        existingPlatform.Manufacturer = platform.Manufacturer;
        existingPlatform.Type = platform.Type;
        existingPlatform.ReleaseDate = platform.ReleaseDate;
        existingPlatform.IsActive = platform.IsActive;

        return existingPlatform;
    }

    /// <summary>
    /// Supprime une plateforme.
    /// </summary>
    /// <param name="id">Identifiant de la plateforme à supprimer</param>
    /// <returns>True si supprimée, false sinon</returns>
    public bool Delete(int id)
    {
        var platform = Get(id);
        if (platform == null)
            return false;

        return _platforms.Remove(platform);
    }

    /// <summary>
    /// Récupère les plateformes actives seulement.
    /// </summary>
    /// <returns>Liste des plateformes actives</returns>
    public IEnumerable<Platform> GetActive()
    {
        return _platforms.Where(p => p.IsActive);
    }

    /// <summary>
    /// Récupère les plateformes par type.
    /// </summary>
    /// <param name="type">Type de plateforme</param>
    /// <returns>Liste des plateformes du type spécifié</returns>
    public IEnumerable<Platform> GetByType(PlatformType type)
    {
        return _platforms.Where(p => p.Type == type);
    }

    /// <summary>
    /// Recherche des plateformes par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des plateformes correspondantes</returns>
    public IEnumerable<Platform> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GetAll();

        return _platforms.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                    (p.ShortName != null && p.ShortName.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }

    /// <summary>
    /// Initialise quelques plateformes de démonstration.
    /// </summary>
    private void InitializeSampleData()
    {
        _platforms.AddRange(new List<Platform>
        {
            new Platform
            {
                Id = _nextId++,
                Name = "Nintendo Switch",
                ShortName = "NSW",
                Manufacturer = "Nintendo",
                Type = PlatformType.Console,
                ReleaseDate = new DateTime(2017, 3, 3),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "PlayStation 5",
                ShortName = "PS5",
                Manufacturer = "Sony",
                Type = PlatformType.Console,
                ReleaseDate = new DateTime(2020, 11, 12),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "Xbox Series X",
                ShortName = "XSX",
                Manufacturer = "Microsoft",
                Type = PlatformType.Console,
                ReleaseDate = new DateTime(2020, 11, 10),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "PC",
                ShortName = "PC",
                Manufacturer = "Various",
                Type = PlatformType.PC,
                ReleaseDate = null,
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "Steam Deck",
                ShortName = "SD",
                Manufacturer = "Valve",
                Type = PlatformType.Handheld,
                ReleaseDate = new DateTime(2022, 2, 25),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "Meta Quest 3",
                ShortName = "MQ3",
                Manufacturer = "Meta",
                Type = PlatformType.VR,
                ReleaseDate = new DateTime(2023, 10, 10),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "iOS",
                ShortName = "iOS",
                Manufacturer = "Apple",
                Type = PlatformType.Mobile,
                ReleaseDate = new DateTime(2007, 6, 29),
                IsActive = true
            },
            new Platform
            {
                Id = _nextId++,
                Name = "Android",
                ShortName = "AND",
                Manufacturer = "Google",
                Type = PlatformType.Mobile,
                ReleaseDate = new DateTime(2008, 9, 23),
                IsActive = true
            }
        });
    }
}