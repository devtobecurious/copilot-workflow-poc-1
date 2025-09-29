using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des éditeurs.
/// </summary>
public class InMemoryPublisherRepository : IPublisherRepository
{
    private readonly List<Publisher> _publishers = new();
    private int _nextId = 1;

    /// <summary>
    /// Constructeur qui initialise quelques éditeurs de démonstration.
    /// </summary>
    public InMemoryPublisherRepository()
    {
        InitializeSampleData();
    }

    /// <summary>
    /// Récupère tous les éditeurs.
    /// </summary>
    /// <returns>Liste de tous les éditeurs</returns>
    public IEnumerable<Publisher> GetAll()
    {
        return _publishers.ToList();
    }

    /// <summary>
    /// Récupère un éditeur par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur</param>
    /// <returns>L'éditeur trouvé ou null</returns>
    public Publisher? Get(int id)
    {
        return _publishers.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Ajoute un nouvel éditeur.
    /// </summary>
    /// <param name="publisher">L'éditeur à ajouter</param>
    /// <returns>L'éditeur ajouté avec son ID généré</returns>
    public Publisher Add(Publisher publisher)
    {
        publisher.Id = _nextId++;
        publisher.CreatedAt = DateTime.UtcNow;
        publisher.UpdatedAt = DateTime.UtcNow;
        _publishers.Add(publisher);
        return publisher;
    }

    /// <summary>
    /// Met à jour un éditeur existant.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur à mettre à jour</param>
    /// <param name="publisher">Les nouvelles données de l'éditeur</param>
    /// <returns>L'éditeur mis à jour ou null si non trouvé</returns>
    public Publisher? Update(int id, Publisher publisher)
    {
        var existingPublisher = Get(id);
        if (existingPublisher == null)
            return null;

        existingPublisher.Name = publisher.Name;
        existingPublisher.Website = publisher.Website;
        existingPublisher.Country = publisher.Country;
        existingPublisher.FoundedYear = publisher.FoundedYear;
        existingPublisher.Description = publisher.Description;
        existingPublisher.LogoUrl = publisher.LogoUrl;
        existingPublisher.IsActive = publisher.IsActive;
        existingPublisher.UpdatedAt = DateTime.UtcNow;

        return existingPublisher;
    }

    /// <summary>
    /// Supprime un éditeur.
    /// </summary>
    /// <param name="id">Identifiant de l'éditeur à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    public bool Delete(int id)
    {
        var publisher = Get(id);
        if (publisher == null)
            return false;

        return _publishers.Remove(publisher);
    }

    /// <summary>
    /// Récupère les éditeurs actifs seulement.
    /// </summary>
    /// <returns>Liste des éditeurs actifs</returns>
    public IEnumerable<Publisher> GetActive()
    {
        return _publishers.Where(p => p.IsActive);
    }

    /// <summary>
    /// Recherche des éditeurs par nom.
    /// </summary>
    /// <param name="name">Nom ou partie du nom à rechercher</param>
    /// <returns>Liste des éditeurs correspondants</returns>
    public IEnumerable<Publisher> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GetAll();

        return _publishers.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Récupère les éditeurs par pays.
    /// </summary>
    /// <param name="country">Nom du pays</param>
    /// <returns>Liste des éditeurs du pays spécifié</returns>
    public IEnumerable<Publisher> GetByCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Enumerable.Empty<Publisher>();

        return _publishers.Where(p => !string.IsNullOrEmpty(p.Country) &&
                                     p.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Initialise quelques éditeurs de démonstration.
    /// </summary>
    private void InitializeSampleData()
    {
        _publishers.AddRange(new List<Publisher>
        {
            new Publisher
            {
                Id = _nextId++,
                Name = "Nintendo",
                Website = "https://www.nintendo.com",
                Country = "Japan",
                FoundedYear = 1889,
                Description = "Entreprise japonaise de jeux vidéo et de consoles de jeux.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "CD Projekt",
                Website = "https://www.cdprojekt.com",
                Country = "Poland",
                FoundedYear = 1994,
                Description = "Studio de développement polonais connu pour The Witcher et Cyberpunk 2077.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Sony Interactive Entertainment",
                Website = "https://www.sie.com",
                Country = "Japan",
                FoundedYear = 1993,
                Description = "Filiale de Sony responsable des PlayStation et de leurs jeux exclusifs.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Microsoft Studios",
                Website = "https://www.xbox.com",
                Country = "United States",
                FoundedYear = 2000,
                Description = "Division de jeux vidéo de Microsoft, éditeur des jeux Xbox.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Ubisoft",
                Website = "https://www.ubisoft.com",
                Country = "France",
                FoundedYear = 1986,
                Description = "Éditeur français de jeux vidéo, connu pour Assassin's Creed, Far Cry.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Electronic Arts",
                Website = "https://www.ea.com",
                Country = "United States",
                FoundedYear = 1982,
                Description = "Grand éditeur américain de jeux vidéo, connu pour FIFA, Battlefield.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Activision Blizzard",
                Website = "https://www.activisionblizzard.com",
                Country = "United States",
                FoundedYear = 2008,
                Description = "Éditeur américain résultant de la fusion d'Activision et Blizzard Entertainment.",
                IsActive = true
            },
            new Publisher
            {
                Id = _nextId++,
                Name = "Valve Corporation",
                Website = "https://www.valvesoftware.com",
                Country = "United States",
                FoundedYear = 1996,
                Description = "Développeur et éditeur américain, créateur de Steam et Half-Life.",
                IsActive = true
            }
        });
    }
}