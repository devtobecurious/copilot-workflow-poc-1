using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Extensions pour configurer les endpoints de gestion des genres.
/// </summary>
public static class GenreEndpoints
{
    /// <summary>
    /// Mappe les endpoints des genres.
    /// </summary>
    /// <param name="routes">Le générateur de routes</param>
    public static void MapGenreEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/genres")
            .WithTags("Genres");

        // GET /api/genres - Récupérer tous les genres
        group.MapGet("/", (IGenreRepository repository) =>
        {
            var genres = repository.GetAll().Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });

            return Results.Ok(genres);
        })
        .WithName("GetGenres")
        .WithSummary("Récupère tous les genres")
        .WithDescription("Retourne une liste de tous les genres de jeux vidéo disponibles.");

        // GET /api/genres/{id} - Récupérer un genre par ID
        group.MapGet("/{id:int}", (int id, IGenreRepository repository) =>
        {
            var genre = repository.Get(id);
            if (genre == null)
                return Results.NotFound($"Genre avec l'ID {id} non trouvé");

            var response = new GenreResponseDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };

            return Results.Ok(response);
        })
        .WithName("GetGenre")
        .WithSummary("Récupère un genre par son ID")
        .WithDescription("Retourne les détails d'un genre spécifique.");

        // POST /api/genres - Créer un nouveau genre
        group.MapPost("/", (Genre genre, IGenreRepository repository) =>
        {
            var createdGenre = repository.Add(genre);
            return Results.Created($"/api/genres/{createdGenre.Id}", createdGenre);
        })
        .WithName("CreateGenre")
        .WithSummary("Crée un nouveau genre")
        .WithDescription("Ajoute un nouveau genre de jeu vidéo au système.");

        // PUT /api/genres/{id} - Mettre à jour un genre
        group.MapPut("/{id:int}", (int id, Genre genre, IGenreRepository repository) =>
        {
            var updatedGenre = repository.Update(id, genre);
            if (updatedGenre == null)
                return Results.NotFound($"Genre avec l'ID {id} non trouvé");

            return Results.Ok(updatedGenre);
        })
        .WithName("UpdateGenre")
        .WithSummary("Met à jour un genre existant")
        .WithDescription("Modifie les informations d'un genre existant.");

        // DELETE /api/genres/{id} - Supprimer un genre
        group.MapDelete("/{id:int}", (int id, IGenreRepository repository) =>
        {
            var deleted = repository.Delete(id);
            if (!deleted)
                return Results.NotFound($"Genre avec l'ID {id} non trouvé");

            return Results.NoContent();
        })
        .WithName("DeleteGenre")
        .WithSummary("Supprime un genre")
        .WithDescription("Supprime un genre du système.");

        // GET /api/genres/active - Récupérer les genres actifs
        group.MapGet("/active", (IGenreRepository repository) =>
        {
            var genres = repository.GetActive().Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });

            return Results.Ok(genres);
        })
        .WithName("GetActiveGenres")
        .WithSummary("Récupère les genres actifs")
        .WithDescription("Retourne uniquement les genres actuellement actifs.");

        // GET /api/genres/search?name={name} - Rechercher par nom
        group.MapGet("/search", ([FromQuery] string? name, IGenreRepository repository) =>
        {
            var genres = string.IsNullOrWhiteSpace(name) 
                ? repository.GetAll() 
                : repository.SearchByName(name);

            var results = genres.Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });

            return Results.Ok(results);
        })
        .WithName("SearchGenres")
        .WithSummary("Recherche des genres par nom")
        .WithDescription("Recherche des genres dont le nom contient le terme spécifié.");
    }
}