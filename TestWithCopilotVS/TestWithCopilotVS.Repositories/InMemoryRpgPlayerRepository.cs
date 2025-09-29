using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository pour les joueurs RPG.
/// </summary>
public class InMemoryRpgPlayerRepository : IRpgPlayerRepository
{
    private readonly List<RpgPlayer> _players = new();
    private int _nextId = 1;

    public InMemoryRpgPlayerRepository()
    {
        InitializeTestData();
    }

    public Task<IEnumerable<RpgPlayer>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<RpgPlayer>>(_players.ToList());
    }

    public Task<RpgPlayer?> GetByIdAsync(int id)
    {
        var player = _players.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(player);
    }

    public Task<IEnumerable<RpgPlayer>> GetByCampaignAsync(int campaignId)
    {
        var players = _players.Where(p => p.CampaignId == campaignId).ToList();
        return Task.FromResult<IEnumerable<RpgPlayer>>(players);
    }

    public Task<IEnumerable<RpgPlayer>> GetActiveByCampaignAsync(int campaignId)
    {
        var players = _players.Where(p => p.CampaignId == campaignId && p.IsActive).ToList();
        return Task.FromResult<IEnumerable<RpgPlayer>>(players);
    }

    public Task<RpgPlayer?> GetByUsernameAndCampaignAsync(string username, int campaignId)
    {
        var player = _players.FirstOrDefault(p => 
            p.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
            p.CampaignId == campaignId);
        return Task.FromResult(player);
    }

    public Task<RpgPlayer?> GetByEmailAndCampaignAsync(string email, int campaignId)
    {
        var player = _players.FirstOrDefault(p => 
            p.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && 
            p.CampaignId == campaignId);
        return Task.FromResult(player);
    }

    public Task<IEnumerable<RpgPlayer>> GetGameMastersAsync()
    {
        var gameMasters = _players.Where(p => 
            p.Role == RpgPlayerRole.GameMaster || 
            p.Role == RpgPlayerRole.CoGameMaster).ToList();
        return Task.FromResult<IEnumerable<RpgPlayer>>(gameMasters);
    }

    public Task<RpgPlayer> CreateAsync(RpgPlayer player)
    {
        player.Id = _nextId++;
        player.JoinedAt = DateTime.UtcNow;
        _players.Add(player);
        return Task.FromResult(player);
    }

    public Task<RpgPlayer?> UpdateAsync(RpgPlayer player)
    {
        var existingPlayer = _players.FirstOrDefault(p => p.Id == player.Id);
        if (existingPlayer == null)
            return Task.FromResult<RpgPlayer?>(null);

        // Mise à jour des propriétés
        existingPlayer.Username = player.Username;
        existingPlayer.Email = player.Email;
        existingPlayer.IsActive = player.IsActive;
        existingPlayer.Role = player.Role;
        existingPlayer.ExperienceLevel = player.ExperienceLevel;

        return Task.FromResult<RpgPlayer?>(existingPlayer);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var player = _players.FirstOrDefault(p => p.Id == id);
        if (player == null)
            return Task.FromResult(false);

        _players.Remove(player);
        return Task.FromResult(true);
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _players.Any(p => p.Id == id);
        return Task.FromResult(exists);
    }

    public Task<bool> IsUsernameInUseAsync(string username, int campaignId, int? excludePlayerId = null)
    {
        var isInUse = _players.Any(p => 
            p.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
            p.CampaignId == campaignId && 
            (excludePlayerId == null || p.Id != excludePlayerId));
        return Task.FromResult(isInUse);
    }

    public Task<bool> IsEmailInUseAsync(string email, int campaignId, int? excludePlayerId = null)
    {
        var isInUse = _players.Any(p => 
            p.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && 
            p.CampaignId == campaignId && 
            (excludePlayerId == null || p.Id != excludePlayerId));
        return Task.FromResult(isInUse);
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeTestData()
    {
        var players = new List<RpgPlayer>
        {
            new RpgPlayer
            {
                Id = _nextId++,
                Username = "MasterDM",
                Email = "master@rpg.com",
                CampaignId = 1,
                Role = RpgPlayerRole.GameMaster,
                ExperienceLevel = RpgExperienceLevel.Expert,
                JoinedAt = DateTime.UtcNow.AddDays(-10)
            },
            new RpgPlayer
            {
                Id = _nextId++,
                Username = "PathfinderGM",
                Email = "pathfinder@rpg.com",
                CampaignId = 2,
                Role = RpgPlayerRole.GameMaster,
                ExperienceLevel = RpgExperienceLevel.Advanced,
                JoinedAt = DateTime.UtcNow.AddDays(-5)
            },
            new RpgPlayer
            {
                Id = _nextId++,
                Username = "CyberMaster",
                Email = "cyber@rpg.com",
                CampaignId = 3,
                Role = RpgPlayerRole.GameMaster,
                ExperienceLevel = RpgExperienceLevel.Intermediate,
                JoinedAt = DateTime.UtcNow.AddDays(-3)
            },
            new RpgPlayer
            {
                Id = _nextId++,
                Username = "ElvenRogue",
                Email = "rogue@player.com",
                CampaignId = 1,
                Role = RpgPlayerRole.Player,
                ExperienceLevel = RpgExperienceLevel.Intermediate,
                JoinedAt = DateTime.UtcNow.AddDays(-2)
            },
            new RpgPlayer
            {
                Id = _nextId++,
                Username = "DwarfCleric",
                Email = "cleric@player.com",
                CampaignId = 1,
                Role = RpgPlayerRole.Player,
                ExperienceLevel = RpgExperienceLevel.Beginner,
                JoinedAt = DateTime.UtcNow.AddDays(-1)
            }
        };

        _players.AddRange(players);
    }
}