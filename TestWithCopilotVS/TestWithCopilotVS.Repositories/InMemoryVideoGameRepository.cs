using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des jeux vidéo.
/// </summary>
public class InMemoryVideoGameRepository : IVideoGameRepository
{
    private readonly List<VideoGame> _videoGames = new();
    private int _nextId = 1;

    /// <summary>
    /// Constructeur qui initialise quelques jeux vidéo de démonstration.
    /// </summary>
    public InMemoryVideoGameRepository()
    {
        InitializeSampleData();
    }

    /// <summary>
    /// Récupère tous les jeux vidéo.
    /// </summary>
    /// <returns>Liste de tous les jeux vidéo</returns>
    public IEnumerable<VideoGame> GetAll()
    {
        return _videoGames.ToList();
    }

    /// <summary>
    /// Récupère un jeu vidéo par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo</param>
    /// <returns>Le jeu vidéo trouvé ou null</returns>
    public VideoGame? Get(int id)
    {
        return _videoGames.FirstOrDefault(vg => vg.Id == id);
    }

    /// <summary>
    /// Ajoute un nouveau jeu vidéo.
    /// </summary>
    /// <param name="videoGame">Le jeu vidéo à ajouter</param>
    /// <returns>Le jeu vidéo ajouté avec son ID généré</returns>
    public VideoGame Add(VideoGame videoGame)
    {
        videoGame.Id = _nextId++;
        videoGame.CreatedAt = DateTime.UtcNow;
        videoGame.UpdatedAt = DateTime.UtcNow;
        _videoGames.Add(videoGame);
        return videoGame;
    }

    /// <summary>
    /// Met à jour un jeu vidéo existant.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo à mettre à jour</param>
    /// <param name="videoGame">Les nouvelles données du jeu vidéo</param>
    /// <returns>Le jeu vidéo mis à jour ou null si non trouvé</returns>
    public VideoGame? Update(int id, VideoGame videoGame)
    {
        var existingGame = Get(id);
        if (existingGame == null)
            return null;

        existingGame.Title = videoGame.Title;
        existingGame.Description = videoGame.Description;
        existingGame.PublisherId = videoGame.PublisherId;
        existingGame.Publisher = videoGame.Publisher;
        existingGame.ReleaseDate = videoGame.ReleaseDate;
        existingGame.MetacriticScore = videoGame.MetacriticScore;
        existingGame.Price = videoGame.Price;
        existingGame.IsAvailable = videoGame.IsAvailable;
        existingGame.UpdatedAt = DateTime.UtcNow;
        existingGame.Genres = videoGame.Genres;
        existingGame.Platforms = videoGame.Platforms;
        existingGame.CoverImageUrl = videoGame.CoverImageUrl;
        existingGame.EsrbRating = videoGame.EsrbRating;

        return existingGame;
    }

    /// <summary>
    /// Supprime un jeu vidéo.
    /// </summary>
    /// <param name="id">Identifiant du jeu vidéo à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    public bool Delete(int id)
    {
        var videoGame = Get(id);
        if (videoGame == null)
            return false;

        return _videoGames.Remove(videoGame);
    }

    /// <summary>
    /// Recherche des jeux vidéo par titre.
    /// </summary>
    /// <param name="title">Titre ou partie du titre à rechercher</param>
    /// <returns>Liste des jeux vidéo correspondants</returns>
    public IEnumerable<VideoGame> SearchByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return GetAll();

        return _videoGames.Where(vg => vg.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Récupère les jeux vidéo par genre.
    /// </summary>
    /// <param name="genreId">Identifiant du genre</param>
    /// <returns>Liste des jeux vidéo du genre spécifié</returns>
    public IEnumerable<VideoGame> GetByGenre(int genreId)
    {
        return _videoGames.Where(vg => vg.Genres.Any(g => g.Id == genreId));
    }

    /// <summary>
    /// Récupère les jeux vidéo par plateforme.
    /// </summary>
    /// <param name="platformId">Identifiant de la plateforme</param>
    /// <returns>Liste des jeux vidéo de la plateforme spécifiée</returns>
    public IEnumerable<VideoGame> GetByPlatform(int platformId)
    {
        return _videoGames.Where(vg => vg.Platforms.Any(p => p.Id == platformId));
    }

    /// <summary>
    /// Récupère les jeux vidéo par éditeur.
    /// </summary>
    /// <param name="publisherId">Identifiant de l'éditeur</param>
    /// <returns>Liste des jeux vidéo de l'éditeur spécifié</returns>
    public IEnumerable<VideoGame> GetByPublisher(int publisherId)
    {
        return _videoGames.Where(vg => vg.PublisherId == publisherId);
    }

    /// <summary>
    /// Récupère les jeux vidéo disponibles seulement.
    /// </summary>
    /// <returns>Liste des jeux vidéo disponibles</returns>
    public IEnumerable<VideoGame> GetAvailable()
    {
        return _videoGames.Where(vg => vg.IsAvailable);
    }

    /// <summary>
    /// Récupère les jeux vidéo dans une fourchette de prix.
    /// </summary>
    /// <param name="minPrice">Prix minimum</param>
    /// <param name="maxPrice">Prix maximum</param>
    /// <returns>Liste des jeux vidéo dans la fourchette de prix</returns>
    public IEnumerable<VideoGame> GetByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _videoGames.Where(vg => vg.Price >= minPrice && vg.Price <= maxPrice);
    }

    /// <summary>
    /// Initialise quelques jeux vidéo de démonstration.
    /// </summary>
    private void InitializeSampleData()
    {
        var nintendo = new Publisher
        {
            Id = 1,
            Name = "Nintendo",
            Website = "https://www.nintendo.com",
            Country = "Japan",
            FoundedYear = 1889,
            IsActive = true
        };

        var cdProjekt = new Publisher
        {
            Id = 2,
            Name = "CD Projekt",
            Website = "https://www.cdprojekt.com",
            Country = "Poland",
            FoundedYear = 1994,
            IsActive = true
        };

        var actionGenre = new Genre { Id = 1, Name = "Action", Description = "Jeux d'action", IsActive = true };
        var rpgGenre = new Genre { Id = 2, Name = "RPG", Description = "Jeux de rôle", IsActive = true };
        var adventureGenre = new Genre { Id = 3, Name = "Adventure", Description = "Jeux d'aventure", IsActive = true };

        var switchPlatform = new Platform { Id = 1, Name = "Nintendo Switch", ShortName = "NSW", Type = PlatformType.Console, IsActive = true };
        var pcPlatform = new Platform { Id = 2, Name = "PC", ShortName = "PC", Type = PlatformType.PC, IsActive = true };
        var ps5Platform = new Platform { Id = 3, Name = "PlayStation 5", ShortName = "PS5", Type = PlatformType.Console, IsActive = true };

        _videoGames.AddRange(new List<VideoGame>
        {
            new VideoGame
            {
                Id = _nextId++,
                Title = "The Legend of Zelda: Breath of the Wild",
                Description = "Un jeu d'aventure en monde ouvert révolutionnaire.",
                PublisherId = 1,
                Publisher = nintendo,
                ReleaseDate = new DateTime(2017, 3, 3),
                MetacriticScore = 97,
                Price = 59.99m,
                IsAvailable = true,
                EsrbRating = EsrbRating.Everyone10Plus,
                Genres = new List<Genre> { actionGenre, adventureGenre },
                Platforms = new List<Platform> { switchPlatform },
                CoverImageUrl = "https://example.com/zelda-botw.jpg"
            },
            new VideoGame
            {
                Id = _nextId++,
                Title = "Cyberpunk 2077",
                Description = "Un RPG futuriste dans un monde cyberpunk.",
                PublisherId = 2,
                Publisher = cdProjekt,
                ReleaseDate = new DateTime(2020, 12, 10),
                MetacriticScore = 86,
                Price = 39.99m,
                IsAvailable = true,
                EsrbRating = EsrbRating.Mature,
                Genres = new List<Genre> { rpgGenre, actionGenre },
                Platforms = new List<Platform> { pcPlatform, ps5Platform },
                CoverImageUrl = "https://example.com/cyberpunk-2077.jpg"
            },
            new VideoGame
            {
                Id = _nextId++,
                Title = "Super Mario Odyssey",
                Description = "Une aventure 3D colorée avec Mario.",
                PublisherId = 1,
                Publisher = nintendo,
                ReleaseDate = new DateTime(2017, 10, 27),
                MetacriticScore = 97,
                Price = 49.99m,
                IsAvailable = true,
                EsrbRating = EsrbRating.Everyone10Plus,
                Genres = new List<Genre> { adventureGenre, actionGenre },
                Platforms = new List<Platform> { switchPlatform },
                CoverImageUrl = "https://example.com/mario-odyssey.jpg"
            }
        });
    }
}