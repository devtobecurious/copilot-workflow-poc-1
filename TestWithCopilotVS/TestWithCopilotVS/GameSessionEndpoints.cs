
using Microsoft.AspNetCore.Routing;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

// Endpoints sessions de jeux
public static class GameSessionEndpoints
{
    public static void MapGameSessionEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/gamesessions", () =>
        {
            var gameSessions = new List<object>
            {
                new { Id = 1, StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddHours(-1), Winner = "Alice", Game = "Chess" },
                new { Id = 2, StartDate = DateTime.Now.AddHours(-3), EndDate = DateTime.Now.AddHours(-2), Winner = "Bob", Game = "Monopoly" }
            };
            return Results.Ok(gameSessions);
        });
    }
}

// Endpoints gestion amis
public static class FriendEndpoints
{
    public static void MapFriendEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/friends", (IFriendRepository repo) => Results.Ok(repo.GetAll()));
        routes.MapGet("/friends/{id:int}", (int id, IFriendRepository repo) =>
        {
            var friend = repo.Get(id);
            return friend is not null ? Results.Ok(friend) : Results.NotFound();
        });
        routes.MapPost("/friends", (Friend newFriend, IFriendRepository repo) =>
        {
            var created = repo.Add(new Friend(0, newFriend.Name, newFriend.Email));
            return Results.Created($"/friends/{created.Id}", created);
        });
        routes.MapPut("/friends/{id:int}", (int id, Friend updated, IFriendRepository repo) =>
        {
            var result = repo.Update(id, updated);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });
        routes.MapDelete("/friends/{id:int}", (int id, IFriendRepository repo) =>
        {
            var deleted = repo.Delete(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        /// <summary>
        /// Récupère la liste de tous les decks Magix de tous les amis.
        /// </summary>
        /// <remarks>
        /// Retourne une liste d'objets DeckMagix (Id, Name, FriendId).
        /// </remarks>
        routes.MapGet("/friends/decks-magix", (IFriendRepository repo) =>
        {
            var decks = repo.GetAllDecksMagix();
            return Results.Ok(decks);
        });
    }
    /// <summary>
    /// Mappe les endpoints statistiques de jeu.
    /// </summary>
    public static void MapStatistiqueEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/stats", (IStatistiqueRepository repo) =>
        {
            var stats = repo.GetAll();
            return Results.Ok(stats);
        });
    }
}