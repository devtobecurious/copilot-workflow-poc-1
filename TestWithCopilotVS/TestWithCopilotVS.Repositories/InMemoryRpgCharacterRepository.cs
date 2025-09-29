using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository pour les personnages RPG.
/// </summary>
public class InMemoryRpgCharacterRepository : IRpgCharacterRepository
{
    private readonly List<RpgCharacter> _characters = new();
    private int _nextId = 1;

    public InMemoryRpgCharacterRepository()
    {
        InitializeTestData();
    }

    public Task<IEnumerable<RpgCharacter>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<RpgCharacter>>(_characters.ToList());
    }

    public Task<RpgCharacter?> GetByIdAsync(int id)
    {
        var character = _characters.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(character);
    }

    public Task<IEnumerable<RpgCharacter>> GetByPlayerAsync(int playerId)
    {
        var characters = _characters.Where(c => c.PlayerId == playerId).ToList();
        return Task.FromResult<IEnumerable<RpgCharacter>>(characters);
    }

    public Task<IEnumerable<RpgCharacter>> GetByCampaignAsync(int campaignId)
    {
        var characters = _characters.Where(c => c.CampaignId == campaignId).ToList();
        return Task.FromResult<IEnumerable<RpgCharacter>>(characters);
    }

    public Task<IEnumerable<RpgCharacter>> GetActiveByCampaignAsync(int campaignId)
    {
        var characters = _characters.Where(c => c.CampaignId == campaignId && c.IsActive).ToList();
        return Task.FromResult<IEnumerable<RpgCharacter>>(characters);
    }

    public Task<RpgCharacter?> GetMainCharacterAsync(int playerId, int campaignId)
    {
        var character = _characters.FirstOrDefault(c => 
            c.PlayerId == playerId && 
            c.CampaignId == campaignId && 
            c.IsMainCharacter && 
            c.IsActive);
        return Task.FromResult(character);
    }

    public Task<IEnumerable<RpgCharacter>> GetByClassAsync(string characterClass, int? campaignId = null)
    {
        var query = _characters.Where(c => 
            c.CharacterClass.Equals(characterClass, StringComparison.OrdinalIgnoreCase));

        if (campaignId.HasValue)
            query = query.Where(c => c.CampaignId == campaignId.Value);

        return Task.FromResult<IEnumerable<RpgCharacter>>(query.ToList());
    }

    public Task<IEnumerable<RpgCharacter>> GetByLevelRangeAsync(int minLevel, int maxLevel, int? campaignId = null)
    {
        var query = _characters.Where(c => c.Level >= minLevel && c.Level <= maxLevel);

        if (campaignId.HasValue)
            query = query.Where(c => c.CampaignId == campaignId.Value);

        return Task.FromResult<IEnumerable<RpgCharacter>>(query.ToList());
    }

    public Task<RpgCharacter> CreateAsync(RpgCharacter character)
    {
        character.Id = _nextId++;
        character.CreatedAt = DateTime.UtcNow;
        character.CurrentHitPoints = character.MaxHitPoints;
        _characters.Add(character);
        return Task.FromResult(character);
    }

    public Task<RpgCharacter?> UpdateAsync(RpgCharacter character)
    {
        var existingCharacter = _characters.FirstOrDefault(c => c.Id == character.Id);
        if (existingCharacter == null)
            return Task.FromResult<RpgCharacter?>(null);

        // Mise à jour des propriétés
        existingCharacter.Name = character.Name;
        existingCharacter.CharacterClass = character.CharacterClass;
        existingCharacter.Race = character.Race;
        existingCharacter.Level = character.Level;
        existingCharacter.ExperiencePoints = character.ExperiencePoints;
        existingCharacter.CurrentHitPoints = character.CurrentHitPoints;
        existingCharacter.MaxHitPoints = character.MaxHitPoints;
        existingCharacter.IsActive = character.IsActive;
        existingCharacter.IsMainCharacter = character.IsMainCharacter;
        existingCharacter.Background = character.Background;
        existingCharacter.Notes = character.Notes;

        return Task.FromResult<RpgCharacter?>(existingCharacter);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var character = _characters.FirstOrDefault(c => c.Id == id);
        if (character == null)
            return Task.FromResult(false);

        _characters.Remove(character);
        return Task.FromResult(true);
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _characters.Any(c => c.Id == id);
        return Task.FromResult(exists);
    }

    public Task<bool> BelongsToPlayerAsync(int characterId, int playerId)
    {
        var belongs = _characters.Any(c => c.Id == characterId && c.PlayerId == playerId);
        return Task.FromResult(belongs);
    }

    public Task<bool> UpdateHitPointsAsync(int characterId, int currentHitPoints)
    {
        var character = _characters.FirstOrDefault(c => c.Id == characterId);
        if (character == null || currentHitPoints < 0)
            return Task.FromResult(false);

        character.CurrentHitPoints = Math.Min(currentHitPoints, character.MaxHitPoints);
        return Task.FromResult(true);
    }

    public Task<bool> AddExperienceAsync(int characterId, int experiencePoints)
    {
        var character = _characters.FirstOrDefault(c => c.Id == characterId);
        if (character == null || experiencePoints < 0)
            return Task.FromResult(false);

        character.ExperiencePoints += experiencePoints;
        return Task.FromResult(true);
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeTestData()
    {
        var characters = new List<RpgCharacter>
        {
            new RpgCharacter
            {
                Id = _nextId++,
                Name = "Aelindra Silverblade",
                CharacterClass = "Rogue",
                Race = "Elf",
                Level = 3,
                ExperiencePoints = 900,
                CurrentHitPoints = 24,
                MaxHitPoints = 24,
                PlayerId = 4, // ElvenRogue
                CampaignId = 1,
                IsActive = true,
                IsMainCharacter = true,
                Background = "Une elfe voleur experte en infiltration et en combat à distance.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new RpgCharacter
            {
                Id = _nextId++,
                Name = "Thorin Forgebeard",
                CharacterClass = "Cleric",
                Race = "Dwarf",
                Level = 2,
                ExperiencePoints = 300,
                CurrentHitPoints = 18,
                MaxHitPoints = 18,
                PlayerId = 5, // DwarfCleric
                CampaignId = 1,
                IsActive = true,
                IsMainCharacter = true,
                Background = "Un clerc nain dévoué à son dieu forgeron, spécialisé dans la guérison et la forge.",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };

        _characters.AddRange(characters);
    }
}