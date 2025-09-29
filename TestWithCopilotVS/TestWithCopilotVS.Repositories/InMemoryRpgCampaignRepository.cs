using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository pour les campagnes RPG.
/// </summary>
public class InMemoryRpgCampaignRepository : IRpgCampaignRepository
{
    private readonly List<RpgCampaign> _campaigns = new();
    private readonly Func<IRpgPlayerRepository> _playerRepositoryFactory;
    private int _nextId = 1;

    public InMemoryRpgCampaignRepository(Func<IRpgPlayerRepository> playerRepositoryFactory)
    {
        _playerRepositoryFactory = playerRepositoryFactory;
        InitializeTestData();
    }

    public Task<IEnumerable<RpgCampaign>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<RpgCampaign>>(_campaigns.ToList());
    }

    public Task<RpgCampaign?> GetByIdAsync(int id)
    {
        var campaign = _campaigns.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(campaign);
    }

    public Task<IEnumerable<RpgCampaign>> GetActiveCampaignsAsync()
    {
        var activeCampaigns = _campaigns.Where(c => c.IsActive).ToList();
        return Task.FromResult<IEnumerable<RpgCampaign>>(activeCampaigns);
    }

    public Task<IEnumerable<RpgCampaign>> GetByGameMasterAsync(int gameMasterId)
    {
        var campaigns = _campaigns.Where(c => c.GameMasterId == gameMasterId).ToList();
        return Task.FromResult<IEnumerable<RpgCampaign>>(campaigns);
    }

    public Task<IEnumerable<RpgCampaign>> GetByGameSystemAsync(string gameSystem)
    {
        var campaigns = _campaigns.Where(c => 
            c.GameSystem.Equals(gameSystem, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult<IEnumerable<RpgCampaign>>(campaigns);
    }

    public Task<RpgCampaign> CreateAsync(RpgCampaign campaign)
    {
        campaign.Id = _nextId++;
        campaign.CreatedAt = DateTime.UtcNow;
        _campaigns.Add(campaign);
        return Task.FromResult(campaign);
    }

    public Task<RpgCampaign?> UpdateAsync(RpgCampaign campaign)
    {
        var existingCampaign = _campaigns.FirstOrDefault(c => c.Id == campaign.Id);
        if (existingCampaign == null)
            return Task.FromResult<RpgCampaign?>(null);

        // Mise à jour des propriétés
        existingCampaign.Title = campaign.Title;
        existingCampaign.Description = campaign.Description;
        existingCampaign.GameSystem = campaign.GameSystem;
        existingCampaign.StartDate = campaign.StartDate;
        existingCampaign.EndDate = campaign.EndDate;
        existingCampaign.IsActive = campaign.IsActive;
        existingCampaign.MinimumLevel = campaign.MinimumLevel;
        existingCampaign.MaximumLevel = campaign.MaximumLevel;
        existingCampaign.MaxPlayers = campaign.MaxPlayers;

        return Task.FromResult<RpgCampaign?>(existingCampaign);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var campaign = _campaigns.FirstOrDefault(c => c.Id == id);
        if (campaign == null)
            return Task.FromResult(false);

        _campaigns.Remove(campaign);
        return Task.FromResult(true);
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _campaigns.Any(c => c.Id == id);
        return Task.FromResult(exists);
    }

    public async Task<int> GetActivePlayerCountAsync(int campaignId)
    {
        var playerRepository = _playerRepositoryFactory();
        var players = await playerRepository.GetActiveByCampaignAsync(campaignId);
        return players.Count();
    }

    public async Task<bool> CanAcceptNewPlayerAsync(int campaignId)
    {
        var campaign = await GetByIdAsync(campaignId);
        if (campaign == null || !campaign.IsActive)
            return false;

        var currentPlayerCount = await GetActivePlayerCountAsync(campaignId);
        return currentPlayerCount < campaign.MaxPlayers;
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeTestData()
    {
        var campaigns = new List<RpgCampaign>
        {
            new RpgCampaign
            {
                Id = _nextId++,
                Title = "Les Mines de Phandelver",
                Description = "Une campagne d'introduction pour D&D 5e se déroulant dans la région de Phandalin.",
                GameSystem = "D&D 5e",
                GameMasterId = 1,
                StartDate = DateTime.UtcNow.AddDays(7),
                IsActive = true,
                MinimumLevel = 1,
                MaximumLevel = 5,
                MaxPlayers = 5,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new RpgCampaign
            {
                Id = _nextId++,
                Title = "L'Éveil des Seigneurs des Runes",
                Description = "Campagne épique Pathfinder dans les Royaumes du Nord.",
                GameSystem = "Pathfinder 2e",
                GameMasterId = 2,
                StartDate = DateTime.UtcNow.AddDays(14),
                IsActive = true,
                MinimumLevel = 1,
                MaximumLevel = 20,
                MaxPlayers = 6,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new RpgCampaign
            {
                Id = _nextId++,
                Title = "Cyberpunk 2077 - Night City",
                Description = "Aventures dans les rues dangereuses de Night City.",
                GameSystem = "Cyberpunk Red",
                GameMasterId = 3,
                StartDate = DateTime.UtcNow.AddDays(21),
                IsActive = true,
                MinimumLevel = 1,
                MaximumLevel = 10,
                MaxPlayers = 4,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

        _campaigns.AddRange(campaigns);
    }
}