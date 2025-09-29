using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Extensions pour configurer les endpoints de gestion des jeux vidéo.
/// </summary>
public static class VideoGameEndpoints
{
    /// <summary>
    /// Mappe les endpoints des jeux vidéo.
    /// </summary>
    /// <param name="routes">Le générateur de routes</param>
    public static void MapVideoGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/videogames")
            .WithTags("VideoGames");

        // GET /api/videogames - Récupérer tous les jeux vidéo
        group.MapGet("/", (IVideoGameRepository repository) =>
        {
            var videoGames = repository.GetAll().Select(vg => new VideoGameSummaryDto
            {
                Id = vg.Id,
                Title = vg.Title,
                PublisherName = vg.Publisher.Name,
                ReleaseDate = vg.ReleaseDate,
                Price = vg.Price,
                MetacriticScore = vg.MetacriticScore,
                IsAvailable = vg.IsAvailable,
                CoverImageUrl = vg.CoverImageUrl,
                EsrbRating = vg.EsrbRating
            });

            return Results.Ok(videoGames);
        })
        .WithName("GetVideoGames")
        .WithSummary("Récupère tous les jeux vidéo")
        .WithDescription("Retourne une liste de tous les jeux vidéo disponibles dans le système.");

        // GET /api/videogames/{id} - Récupérer un jeu vidéo par ID
        group.MapGet("/{id:int}", (int id, IVideoGameRepository repository) =>
        {
            var videoGame = repository.Get(id);
            if (videoGame == null)
                return Results.NotFound($"Jeu vidéo avec l'ID {id} non trouvé");

            var response = new VideoGameResponseDto
            {
                Id = videoGame.Id,
                Title = videoGame.Title,
                Description = videoGame.Description,
                Publisher = new PublisherResponseDto
                {
                    Id = videoGame.Publisher.Id,
                    Name = videoGame.Publisher.Name,
                    Website = videoGame.Publisher.Website,
                    Country = videoGame.Publisher.Country
                },
                ReleaseDate = videoGame.ReleaseDate,
                MetacriticScore = videoGame.MetacriticScore,
                Price = videoGame.Price,
                IsAvailable = videoGame.IsAvailable,
                CreatedAt = videoGame.CreatedAt,
                UpdatedAt = videoGame.UpdatedAt,
                Genres = videoGame.Genres.Select(g => new GenreResponseDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                }).ToList(),
                Platforms = videoGame.Platforms.Select(p => new PlatformResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortName = p.ShortName,
                    Type = p.Type
                }).ToList(),
                CoverImageUrl = videoGame.CoverImageUrl,
                EsrbRating = videoGame.EsrbRating
            };

            return Results.Ok(response);
        })
        .WithName("GetVideoGame")
        .WithSummary("Récupère un jeu vidéo par son ID")
        .WithDescription("Retourne les détails complets d'un jeu vidéo spécifique.");

        // POST /api/videogames - Créer un nouveau jeu vidéo
        group.MapPost("/", (CreateVideoGameDto dto, 
            IVideoGameRepository videoGameRepository,
            IPublisherRepository publisherRepository,
            IGenreRepository genreRepository,
            IPlatformRepository platformRepository) =>
        {
            // Validation du publisher
            var publisher = publisherRepository.Get(dto.PublisherId);
            if (publisher == null)
                return Results.BadRequest($"Éditeur avec l'ID {dto.PublisherId} non trouvé");

            // Validation des genres
            var genres = new List<Genre>();
            foreach (var genreId in dto.GenreIds)
            {
                var genre = genreRepository.Get(genreId);
                if (genre == null)
                    return Results.BadRequest($"Genre avec l'ID {genreId} non trouvé");
                genres.Add(genre);
            }

            // Validation des plateformes
            var platforms = new List<Platform>();
            foreach (var platformId in dto.PlatformIds)
            {
                var platform = platformRepository.Get(platformId);
                if (platform == null)
                    return Results.BadRequest($"Plateforme avec l'ID {platformId} non trouvée");
                platforms.Add(platform);
            }

            var videoGame = new VideoGame
            {
                Title = dto.Title,
                Description = dto.Description,
                PublisherId = dto.PublisherId,
                Publisher = publisher,
                ReleaseDate = dto.ReleaseDate,
                MetacriticScore = dto.MetacriticScore,
                Price = dto.Price,
                Genres = genres,
                Platforms = platforms,
                CoverImageUrl = dto.CoverImageUrl,
                EsrbRating = dto.EsrbRating
            };

            var createdVideoGame = videoGameRepository.Add(videoGame);
            return Results.Created($"/api/videogames/{createdVideoGame.Id}", createdVideoGame);
        })
        .WithName("CreateVideoGame")
        .WithSummary("Crée un nouveau jeu vidéo")
        .WithDescription("Ajoute un nouveau jeu vidéo au système avec les informations fournies.");

        // PUT /api/videogames/{id} - Mettre à jour un jeu vidéo
        group.MapPut("/{id:int}", (int id, UpdateVideoGameDto dto,
            IVideoGameRepository videoGameRepository,
            IPublisherRepository publisherRepository,
            IGenreRepository genreRepository,
            IPlatformRepository platformRepository) =>
        {
            var existingGame = videoGameRepository.Get(id);
            if (existingGame == null)
                return Results.NotFound($"Jeu vidéo avec l'ID {id} non trouvé");

            // Mise à jour des propriétés si elles sont fournies
            if (!string.IsNullOrEmpty(dto.Title))
                existingGame.Title = dto.Title;

            if (dto.Description != null)
                existingGame.Description = dto.Description;

            if (dto.PublisherId.HasValue)
            {
                var publisher = publisherRepository.Get(dto.PublisherId.Value);
                if (publisher == null)
                    return Results.BadRequest($"Éditeur avec l'ID {dto.PublisherId} non trouvé");
                existingGame.PublisherId = dto.PublisherId.Value;
                existingGame.Publisher = publisher;
            }

            if (dto.ReleaseDate.HasValue)
                existingGame.ReleaseDate = dto.ReleaseDate.Value;

            if (dto.MetacriticScore.HasValue)
                existingGame.MetacriticScore = dto.MetacriticScore;

            if (dto.Price.HasValue)
                existingGame.Price = dto.Price.Value;

            if (dto.IsAvailable.HasValue)
                existingGame.IsAvailable = dto.IsAvailable.Value;

            if (dto.CoverImageUrl != null)
                existingGame.CoverImageUrl = dto.CoverImageUrl;

            if (dto.EsrbRating.HasValue)
                existingGame.EsrbRating = dto.EsrbRating.Value;

            // Mise à jour des genres si fournis
            if (dto.GenreIds != null)
            {
                var genres = new List<Genre>();
                foreach (var genreId in dto.GenreIds)
                {
                    var genre = genreRepository.Get(genreId);
                    if (genre == null)
                        return Results.BadRequest($"Genre avec l'ID {genreId} non trouvé");
                    genres.Add(genre);
                }
                existingGame.Genres = genres;
            }

            // Mise à jour des plateformes si fournies
            if (dto.PlatformIds != null)
            {
                var platforms = new List<Platform>();
                foreach (var platformId in dto.PlatformIds)
                {
                    var platform = platformRepository.Get(platformId);
                    if (platform == null)
                        return Results.BadRequest($"Plateforme avec l'ID {platformId} non trouvée");
                    platforms.Add(platform);
                }
                existingGame.Platforms = platforms;
            }

            var updatedGame = videoGameRepository.Update(id, existingGame);
            return Results.Ok(updatedGame);
        })
        .WithName("UpdateVideoGame")
        .WithSummary("Met à jour un jeu vidéo existant")
        .WithDescription("Modifie les informations d'un jeu vidéo existant.");

        // DELETE /api/videogames/{id} - Supprimer un jeu vidéo
        group.MapDelete("/{id:int}", (int id, IVideoGameRepository repository) =>
        {
            var deleted = repository.Delete(id);
            if (!deleted)
                return Results.NotFound($"Jeu vidéo avec l'ID {id} non trouvé");

            return Results.NoContent();
        })
        .WithName("DeleteVideoGame")
        .WithSummary("Supprime un jeu vidéo")
        .WithDescription("Supprime un jeu vidéo du système.");

        // GET /api/videogames/search?title={title} - Rechercher par titre
        group.MapGet("/search", ([FromQuery] string? title, IVideoGameRepository repository) =>
        {
            var videoGames = string.IsNullOrWhiteSpace(title) 
                ? repository.GetAll() 
                : repository.SearchByTitle(title);

            var results = videoGames.Select(vg => new VideoGameSummaryDto
            {
                Id = vg.Id,
                Title = vg.Title,
                PublisherName = vg.Publisher.Name,
                ReleaseDate = vg.ReleaseDate,
                Price = vg.Price,
                MetacriticScore = vg.MetacriticScore,
                IsAvailable = vg.IsAvailable,
                CoverImageUrl = vg.CoverImageUrl,
                EsrbRating = vg.EsrbRating
            });

            return Results.Ok(results);
        })
        .WithName("SearchVideoGames")
        .WithSummary("Recherche des jeux vidéo par titre")
        .WithDescription("Recherche des jeux vidéo dont le titre contient le terme spécifié.");

        // GET /api/videogames/available - Récupérer les jeux disponibles
        group.MapGet("/available", (IVideoGameRepository repository) =>
        {
            var videoGames = repository.GetAvailable().Select(vg => new VideoGameSummaryDto
            {
                Id = vg.Id,
                Title = vg.Title,
                PublisherName = vg.Publisher.Name,
                ReleaseDate = vg.ReleaseDate,
                Price = vg.Price,
                MetacriticScore = vg.MetacriticScore,
                IsAvailable = vg.IsAvailable,
                CoverImageUrl = vg.CoverImageUrl,
                EsrbRating = vg.EsrbRating
            });

            return Results.Ok(videoGames);
        })
        .WithName("GetAvailableVideoGames")
        .WithSummary("Récupère les jeux vidéo disponibles")
        .WithDescription("Retourne uniquement les jeux vidéo actuellement disponibles à la vente.");

        // GET /api/videogames/price-range?min={min}&max={max} - Filtrer par prix
        group.MapGet("/price-range", ([FromQuery] decimal? min, [FromQuery] decimal? max, IVideoGameRepository repository) =>
        {
            var minPrice = min ?? 0m;
            var maxPrice = max ?? decimal.MaxValue;

            var videoGames = repository.GetByPriceRange(minPrice, maxPrice).Select(vg => new VideoGameSummaryDto
            {
                Id = vg.Id,
                Title = vg.Title,
                PublisherName = vg.Publisher.Name,
                ReleaseDate = vg.ReleaseDate,
                Price = vg.Price,
                MetacriticScore = vg.MetacriticScore,
                IsAvailable = vg.IsAvailable,
                CoverImageUrl = vg.CoverImageUrl,
                EsrbRating = vg.EsrbRating
            });

            return Results.Ok(videoGames);
        })
        .WithName("GetVideoGamesByPriceRange")
        .WithSummary("Filtre les jeux vidéo par fourchette de prix")
        .WithDescription("Retourne les jeux vidéo dans la fourchette de prix spécifiée.");
    }
}