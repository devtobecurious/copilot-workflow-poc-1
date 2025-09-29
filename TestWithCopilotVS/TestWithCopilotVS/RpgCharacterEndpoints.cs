using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Endpoints pour la gestion des personnages de jeu de rôle.
/// </summary>
public static class RpgCharacterEndpoints
{
    /// <summary>
    /// Configure les endpoints pour les personnages RPG.
    /// </summary>
    /// <param name="app">Application web.</param>
    public static void MapRpgCharacterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/rpg/characters")
            .WithTags("RPG Characters");

        // GET /api/rpg/characters - Récupère tous les personnages
        group.MapGet("/", GetAllCharacters)
            .WithName("GetAllRpgCharacters")
            .WithSummary("Récupère tous les personnages de jeu de rôle")
            .Produces<IEnumerable<RpgCharacter>>();

        // GET /api/rpg/characters/{id} - Récupère un personnage par ID
        group.MapGet("/{id:int}", GetCharacterById)
            .WithName("GetRpgCharacterById")
            .WithSummary("Récupère un personnage par son identifiant")
            .Produces<RpgCharacter>()
            .Produces(404);

        // GET /api/rpg/characters/campaign/{campaignId} - Personnages d'une campagne
        group.MapGet("/campaign/{campaignId:int}", GetCharactersByCampaign)
            .WithName("GetCharactersByCampaign")
            .WithSummary("Récupère les personnages d'une campagne")
            .Produces<IEnumerable<RpgCharacter>>();

        // GET /api/rpg/characters/player/{playerId} - Personnages d'un joueur
        group.MapGet("/player/{playerId:int}", GetCharactersByPlayer)
            .WithName("GetCharactersByPlayer")
            .WithSummary("Récupère les personnages d'un joueur")
            .Produces<IEnumerable<RpgCharacter>>();

        // POST /api/rpg/characters - Crée un nouveau personnage
        group.MapPost("/", CreateCharacter)
            .WithName("CreateRpgCharacter")
            .WithSummary("Crée un nouveau personnage de jeu de rôle")
            .Produces<RpgCharacter>(201)
            .Produces(400);

        // PUT /api/rpg/characters/{id} - Met à jour un personnage
        group.MapPut("/{id:int}", UpdateCharacter)
            .WithName("UpdateRpgCharacter")
            .WithSummary("Met à jour un personnage existant")
            .Produces<RpgCharacter>()
            .Produces(400)
            .Produces(404);

        // DELETE /api/rpg/characters/{id} - Supprime un personnage
        group.MapDelete("/{id:int}", DeleteCharacter)
            .WithName("DeleteRpgCharacter")
            .WithSummary("Supprime un personnage")
            .Produces(204)
            .Produces(404);
    }

    /// <summary>
    /// Récupère tous les personnages.
    /// </summary>
    private static async Task<IResult> GetAllCharacters(IRpgCharacterRepository repository)
    {
        var characters = await repository.GetAllAsync();
        return Results.Ok(characters);
    }

    /// <summary>
    /// Récupère un personnage par son identifiant.
    /// </summary>
    private static async Task<IResult> GetCharacterById(int id, IRpgCharacterRepository repository)
    {
        var character = await repository.GetByIdAsync(id);
        return character != null ? Results.Ok(character) : Results.NotFound($"Personnage avec l'ID {id} non trouvé");
    }

    /// <summary>
    /// Récupère les personnages d'une campagne.
    /// </summary>
    private static async Task<IResult> GetCharactersByCampaign(int campaignId, IRpgCharacterRepository repository)
    {
        var characters = await repository.GetByCampaignAsync(campaignId);
        return Results.Ok(characters);
    }

    /// <summary>
    /// Récupère les personnages d'un joueur.
    /// </summary>
    private static async Task<IResult> GetCharactersByPlayer(int playerId, IRpgCharacterRepository repository)
    {
        var characters = await repository.GetByPlayerAsync(playerId);
        return Results.Ok(characters);
    }

    /// <summary>
    /// Crée un nouveau personnage.
    /// </summary>
    private static async Task<IResult> CreateCharacter(
        CreateRpgCharacterDto createDto,
        int playerId,
        int campaignId,
        IRpgCharacterRepository repository)
    {
        var character = new RpgCharacter
        {
            Name = createDto.Name,
            CharacterClass = createDto.CharacterClass,
            Race = createDto.Race,
            Level = createDto.Level,
            MaxHitPoints = createDto.MaxHitPoints,
            CurrentHitPoints = createDto.MaxHitPoints,
            PlayerId = playerId,
            CampaignId = campaignId,
            Background = createDto.Background,
            IsMainCharacter = createDto.IsMainCharacter
        };

        var createdCharacter = await repository.CreateAsync(character);
        return Results.Created($"/api/rpg/characters/{createdCharacter.Id}", createdCharacter);
    }

    /// <summary>
    /// Met à jour un personnage existant.
    /// </summary>
    private static async Task<IResult> UpdateCharacter(
        int id,
        RpgCharacter updateCharacter,
        IRpgCharacterRepository repository)
    {
        var existingCharacter = await repository.GetByIdAsync(id);
        if (existingCharacter == null)
            return Results.NotFound($"Personnage avec l'ID {id} non trouvé");

        updateCharacter.Id = id;
        var updatedCharacter = await repository.UpdateAsync(updateCharacter);
        return updatedCharacter != null ? Results.Ok(updatedCharacter) : Results.NotFound();
    }

    /// <summary>
    /// Supprime un personnage.
    /// </summary>
    private static async Task<IResult> DeleteCharacter(int id, IRpgCharacterRepository repository)
    {
        var deleted = await repository.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound($"Personnage avec l'ID {id} non trouvé");
    }
}