using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Tests;

/// <summary>
/// Tests pour le repository des campagnes RPG.
/// </summary>
public class RpgCampaignRepositoryTests
{
    private readonly IRpgCampaignRepository _campaignRepository;
    private readonly IRpgPlayerRepository _playerRepository;

    public RpgCampaignRepositoryTests()
    {
        _playerRepository = new InMemoryRpgPlayerRepository();
        _campaignRepository = new InMemoryRpgCampaignRepository(() => _playerRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCampaigns()
    {
        // Act
        var campaigns = await _campaignRepository.GetAllAsync();

        // Assert
        Assert.NotNull(campaigns);
        Assert.True(campaigns.Count() >= 3); // Les données de test contiennent au moins 3 campagnes
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCampaign()
    {
        // Arrange
        var campaigns = await _campaignRepository.GetAllAsync();
        var firstCampaign = campaigns.First();

        // Act
        var result = await _campaignRepository.GetByIdAsync(firstCampaign.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(firstCampaign.Id, result.Id);
        Assert.Equal(firstCampaign.Title, result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _campaignRepository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidCampaign_ShouldCreateAndReturnCampaign()
    {
        // Arrange
        var newCampaign = new RpgCampaign
        {
            Title = "Test Campaign",
            Description = "Campaign de test",
            GameSystem = "D&D 5e",
            GameMasterId = 1,
            MinimumLevel = 1,
            MaximumLevel = 10,
            MaxPlayers = 4
        };

        // Act
        var result = await _campaignRepository.CreateAsync(newCampaign);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(newCampaign.Title, result.Title);
        Assert.Equal(newCampaign.GameSystem, result.GameSystem);
        Assert.True(result.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task GetActiveCampaignsAsync_ShouldReturnOnlyActiveCampaigns()
    {
        // Arrange - Créer une campagne inactive
        var inactiveCampaign = new RpgCampaign
        {
            Title = "Inactive Campaign",
            Description = "Campaign inactive",
            GameSystem = "Pathfinder",
            GameMasterId = 1,
            IsActive = false
        };
        await _campaignRepository.CreateAsync(inactiveCampaign);

        // Act
        var activeCampaigns = await _campaignRepository.GetActiveCampaignsAsync();

        // Assert
        Assert.NotNull(activeCampaigns);
        Assert.All(activeCampaigns, c => Assert.True(c.IsActive));
        Assert.DoesNotContain(activeCampaigns, c => c.Title == "Inactive Campaign");
    }

    [Fact]
    public async Task GetByGameMasterAsync_ShouldReturnCampaignsForSpecificGM()
    {
        // Arrange
        int gameMasterId = 1;

        // Act
        var campaigns = await _campaignRepository.GetByGameMasterAsync(gameMasterId);

        // Assert
        Assert.NotNull(campaigns);
        Assert.All(campaigns, c => Assert.Equal(gameMasterId, c.GameMasterId));
    }

    [Fact]
    public async Task GetByGameSystemAsync_ShouldReturnCampaignsForSpecificSystem()
    {
        // Arrange
        string gameSystem = "D&D 5e";

        // Act
        var campaigns = await _campaignRepository.GetByGameSystemAsync(gameSystem);

        // Assert
        Assert.NotNull(campaigns);
        Assert.All(campaigns, c => 
            Assert.Equal(gameSystem, c.GameSystem, StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task UpdateAsync_WithValidCampaign_ShouldUpdateAndReturn()
    {
        // Arrange
        var campaigns = await _campaignRepository.GetAllAsync();
        var campaignToUpdate = campaigns.First();
        var originalTitle = campaignToUpdate.Title;
        campaignToUpdate.Title = "Updated Title";

        // Act
        var result = await _campaignRepository.UpdateAsync(campaignToUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.NotEqual(originalTitle, result.Title);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidCampaign_ShouldReturnNull()
    {
        // Arrange
        var invalidCampaign = new RpgCampaign { Id = 999, Title = "Invalid" };

        // Act
        var result = await _campaignRepository.UpdateAsync(invalidCampaign);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var newCampaign = new RpgCampaign
        {
            Title = "Campaign to Delete",
            GameSystem = "Test",
            GameMasterId = 1
        };
        var created = await _campaignRepository.CreateAsync(newCampaign);

        // Act
        var result = await _campaignRepository.DeleteAsync(created.Id);

        // Assert
        Assert.True(result);
        
        // Vérifier que la campagne n'existe plus
        var deleted = await _campaignRepository.GetByIdAsync(created.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _campaignRepository.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var campaigns = await _campaignRepository.GetAllAsync();
        var existingCampaign = campaigns.First();

        // Act
        var result = await _campaignRepository.ExistsAsync(existingCampaign.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _campaignRepository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }
}