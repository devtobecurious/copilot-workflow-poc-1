using Microsoft.AspNetCore.Mvc;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS;

/// <summary>
/// Endpoints pour la gestion des campagnes de jeu de rôle.
/// </summary>
public static class RpgCampaignEndpoints
{
    /// <summary>
    /// Configure les endpoints pour les campagnes RPG.
    /// </summary>
    /// <param name="app">Application web.</param>
    public static void MapRpgCampaignEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/rpg/campaigns")
            .WithTags("RPG Campaigns");

        // GET /api/rpg/campaigns - Récupère toutes les campagnes
        group.MapGet("/", GetAllCampaigns)
            .WithName("GetAllRpgCampaigns")
            .WithSummary("Récupère toutes les campagnes de jeu de rôle")
            .Produces<IEnumerable<RpgCampaign>>();

        // GET /api/rpg/campaigns/active - Récupère les campagnes actives
        group.MapGet("/active", GetActiveCampaigns)
            .WithName("GetActiveCampaigns")
            .WithSummary("Récupère les campagnes actives")
            .Produces<IEnumerable<RpgCampaign>>();

        // GET /api/rpg/campaigns/{id} - Récupère une campagne par ID
        group.MapGet("/{id:int}", GetCampaignById)
            .WithName("GetRpgCampaignById")
            .WithSummary("Récupère une campagne par son identifiant")
            .Produces<RpgCampaign>()
            .Produces(404);

        // GET /api/rpg/campaigns/gamemaster/{gameMasterId} - Campagnes d'un MJ
        group.MapGet("/gamemaster/{gameMasterId:int}", GetCampaignsByGameMaster)
            .WithName("GetCampaignsByGameMaster")
            .WithSummary("Récupère les campagnes d'un maître de jeu")
            .Produces<IEnumerable<RpgCampaign>>();

        // GET /api/rpg/campaigns/system/{gameSystem} - Campagnes par système
        group.MapGet("/system/{gameSystem}", GetCampaignsBySystem)
            .WithName("GetCampaignsBySystem")
            .WithSummary("Récupère les campagnes par système de jeu")
            .Produces<IEnumerable<RpgCampaign>>();

        // POST /api/rpg/campaigns - Crée une nouvelle campagne
        group.MapPost("/", CreateCampaign)
            .WithName("CreateRpgCampaign")
            .WithSummary("Crée une nouvelle campagne de jeu de rôle")
            .Produces<RpgCampaign>(201)
            .Produces(400);

        // PUT /api/rpg/campaigns/{id} - Met à jour une campagne
        group.MapPut("/{id:int}", UpdateCampaign)
            .WithName("UpdateRpgCampaign")
            .WithSummary("Met à jour une campagne existante")
            .Produces<RpgCampaign>()
            .Produces(400)
            .Produces(404);

        // DELETE /api/rpg/campaigns/{id} - Supprime une campagne
        group.MapDelete("/{id:int}", DeleteCampaign)
            .WithName("DeleteRpgCampaign")
            .WithSummary("Supprime une campagne")
            .Produces(204)
            .Produces(404);

        // GET /api/rpg/campaigns/{id}/players/count - Nombre de joueurs
        group.MapGet("/{id:int}/players/count", GetPlayerCount)
            .WithName("GetCampaignPlayerCount")
            .WithSummary("Récupère le nombre de joueurs actifs dans la campagne")
            .Produces<int>()
            .Produces(404);

        // GET /api/rpg/campaigns/{id}/can-accept-players - Peut accepter de nouveaux joueurs
        group.MapGet("/{id:int}/can-accept-players", CanAcceptNewPlayers)
            .WithName("CanCampaignAcceptNewPlayers")
            .WithSummary("Vérifie si la campagne peut accepter de nouveaux joueurs")
            .Produces<bool>()
            .Produces(404);
    }

    /// <summary>
    /// Récupère toutes les campagnes.
    /// </summary>
    private static async Task<IResult> GetAllCampaigns(IRpgCampaignRepository repository)
    {
        var campaigns = await repository.GetAllAsync();
        return Results.Ok(campaigns);
    }

    /// <summary>
    /// Récupère les campagnes actives.
    /// </summary>
    private static async Task<IResult> GetActiveCampaigns(IRpgCampaignRepository repository)
    {
        var campaigns = await repository.GetActiveCampaignsAsync();
        return Results.Ok(campaigns);
    }

    /// <summary>
    /// Récupère une campagne par son identifiant.
    /// </summary>
    private static async Task<IResult> GetCampaignById(int id, IRpgCampaignRepository repository)
    {
        var campaign = await repository.GetByIdAsync(id);
        return campaign != null ? Results.Ok(campaign) : Results.NotFound($"Campagne avec l'ID {id} non trouvée");
    }

    /// <summary>
    /// Récupère les campagnes d'un maître de jeu.
    /// </summary>
    private static async Task<IResult> GetCampaignsByGameMaster(int gameMasterId, IRpgCampaignRepository repository)
    {
        var campaigns = await repository.GetByGameMasterAsync(gameMasterId);
        return Results.Ok(campaigns);
    }

    /// <summary>
    /// Récupère les campagnes par système de jeu.
    /// </summary>
    private static async Task<IResult> GetCampaignsBySystem(string gameSystem, IRpgCampaignRepository repository)
    {
        var campaigns = await repository.GetByGameSystemAsync(gameSystem);
        return Results.Ok(campaigns);
    }

    /// <summary>
    /// Crée une nouvelle campagne.
    /// </summary>
    private static async Task<IResult> CreateCampaign(
        CreateRpgCampaignDto createDto,
        int gameMasterId,
        IRpgCampaignRepository repository)
    {
        var campaign = new RpgCampaign
        {
            Title = createDto.Title,
            Description = createDto.Description,
            GameSystem = createDto.GameSystem,
            GameMasterId = gameMasterId,
            StartDate = createDto.StartDate,
            MinimumLevel = createDto.MinimumLevel,
            MaximumLevel = createDto.MaximumLevel,
            MaxPlayers = createDto.MaxPlayers
        };

        var createdCampaign = await repository.CreateAsync(campaign);
        return Results.Created($"/api/rpg/campaigns/{createdCampaign.Id}", createdCampaign);
    }

    /// <summary>
    /// Met à jour une campagne existante.
    /// </summary>
    private static async Task<IResult> UpdateCampaign(
        int id,
        UpdateRpgCampaignDto updateDto,
        IRpgCampaignRepository repository)
    {
        var existingCampaign = await repository.GetByIdAsync(id);
        if (existingCampaign == null)
            return Results.NotFound($"Campagne avec l'ID {id} non trouvée");

        // Mise à jour sélective des propriétés
        if (!string.IsNullOrEmpty(updateDto.Title))
            existingCampaign.Title = updateDto.Title;
        if (updateDto.Description != null)
            existingCampaign.Description = updateDto.Description;
        if (updateDto.StartDate.HasValue)
            existingCampaign.StartDate = updateDto.StartDate;
        if (updateDto.EndDate.HasValue)
            existingCampaign.EndDate = updateDto.EndDate;
        if (updateDto.IsActive.HasValue)
            existingCampaign.IsActive = updateDto.IsActive.Value;
        if (updateDto.MaxPlayers.HasValue)
            existingCampaign.MaxPlayers = updateDto.MaxPlayers.Value;

        var updatedCampaign = await repository.UpdateAsync(existingCampaign);
        return updatedCampaign != null ? Results.Ok(updatedCampaign) : Results.NotFound();
    }

    /// <summary>
    /// Supprime une campagne.
    /// </summary>
    private static async Task<IResult> DeleteCampaign(int id, IRpgCampaignRepository repository)
    {
        var deleted = await repository.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound($"Campagne avec l'ID {id} non trouvée");
    }

    /// <summary>
    /// Récupère le nombre de joueurs actifs dans la campagne.
    /// </summary>
    private static async Task<IResult> GetPlayerCount(int id, IRpgCampaignRepository repository)
    {
        var exists = await repository.ExistsAsync(id);
        if (!exists)
            return Results.NotFound($"Campagne avec l'ID {id} non trouvée");

        var count = await repository.GetActivePlayerCountAsync(id);
        return Results.Ok(count);
    }

    /// <summary>
    /// Vérifie si la campagne peut accepter de nouveaux joueurs.
    /// </summary>
    private static async Task<IResult> CanAcceptNewPlayers(int id, IRpgCampaignRepository repository)
    {
        var exists = await repository.ExistsAsync(id);
        if (!exists)
            return Results.NotFound($"Campagne avec l'ID {id} non trouvée");

        var canAccept = await repository.CanAcceptNewPlayerAsync(id);
        return Results.Ok(canAccept);
    }
}