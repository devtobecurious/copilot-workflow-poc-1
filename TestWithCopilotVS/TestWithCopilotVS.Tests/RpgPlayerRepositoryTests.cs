using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Tests;

/// <summary>
/// Tests pour le repository des joueurs RPG.
/// </summary>
public class RpgPlayerRepositoryTests
{
    private readonly IRpgPlayerRepository _playerRepository;

    public RpgPlayerRepositoryTests()
    {
        _playerRepository = new InMemoryRpgPlayerRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPlayers()
    {
        // Act
        var players = await _playerRepository.GetAllAsync();

        // Assert
        Assert.NotNull(players);
        Assert.True(players.Count() >= 5); // Les données de test contiennent au moins 5 joueurs
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnPlayer()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var firstPlayer = players.First();

        // Act
        var result = await _playerRepository.GetByIdAsync(firstPlayer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(firstPlayer.Id, result.Id);
        Assert.Equal(firstPlayer.Username, result.Username);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _playerRepository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidPlayer_ShouldCreateAndReturnPlayer()
    {
        // Arrange
        var newPlayer = new RpgPlayer
        {
            Username = "NewTestPlayer",
            Email = "newplayer@test.com",
            CampaignId = 1,
            Role = RpgPlayerRole.Player,
            ExperienceLevel = RpgExperienceLevel.Beginner
        };

        // Act
        var result = await _playerRepository.CreateAsync(newPlayer);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(newPlayer.Username, result.Username);
        Assert.Equal(newPlayer.Email, result.Email);
        Assert.True(result.JoinedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task GetByCampaignAsync_ShouldReturnPlayersForSpecificCampaign()
    {
        // Arrange
        int campaignId = 1;

        // Act
        var players = await _playerRepository.GetByCampaignAsync(campaignId);

        // Assert
        Assert.NotNull(players);
        Assert.All(players, p => Assert.Equal(campaignId, p.CampaignId));
    }

    [Fact]
    public async Task GetActiveByCampaignAsync_ShouldReturnOnlyActivePlayers()
    {
        // Arrange
        int campaignId = 1;
        
        // Créer un joueur inactif
        var inactivePlayer = new RpgPlayer
        {
            Username = "InactivePlayer",
            Email = "inactive@test.com",
            CampaignId = campaignId,
            IsActive = false
        };
        await _playerRepository.CreateAsync(inactivePlayer);

        // Act
        var activePlayers = await _playerRepository.GetActiveByCampaignAsync(campaignId);

        // Assert
        Assert.NotNull(activePlayers);
        Assert.All(activePlayers, p => Assert.True(p.IsActive));
        Assert.All(activePlayers, p => Assert.Equal(campaignId, p.CampaignId));
        Assert.DoesNotContain(activePlayers, p => p.Username == "InactivePlayer");
    }

    [Fact]
    public async Task GetByUsernameAndCampaignAsync_WithValidData_ShouldReturnPlayer()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var existingPlayer = players.First();

        // Act
        var result = await _playerRepository.GetByUsernameAndCampaignAsync(
            existingPlayer.Username, existingPlayer.CampaignId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingPlayer.Username, result.Username);
        Assert.Equal(existingPlayer.CampaignId, result.CampaignId);
    }

    [Fact]
    public async Task GetByUsernameAndCampaignAsync_WithInvalidData_ShouldReturnNull()
    {
        // Act
        var result = await _playerRepository.GetByUsernameAndCampaignAsync("InvalidUser", 999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByEmailAndCampaignAsync_WithValidData_ShouldReturnPlayer()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var existingPlayer = players.First();

        // Act
        var result = await _playerRepository.GetByEmailAndCampaignAsync(
            existingPlayer.Email, existingPlayer.CampaignId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingPlayer.Email, result.Email);
        Assert.Equal(existingPlayer.CampaignId, result.CampaignId);
    }

    [Fact]
    public async Task GetGameMastersAsync_ShouldReturnOnlyGameMasters()
    {
        // Act
        var gameMasters = await _playerRepository.GetGameMastersAsync();

        // Assert
        Assert.NotNull(gameMasters);
        Assert.All(gameMasters, gm => 
            Assert.True(gm.Role == RpgPlayerRole.GameMaster || gm.Role == RpgPlayerRole.CoGameMaster));
    }

    [Fact]
    public async Task UpdateAsync_WithValidPlayer_ShouldUpdateAndReturn()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var playerToUpdate = players.First();
        var originalUsername = playerToUpdate.Username;
        playerToUpdate.Username = "UpdatedUsername";

        // Act
        var result = await _playerRepository.UpdateAsync(playerToUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdatedUsername", result.Username);
        Assert.NotEqual(originalUsername, result.Username);
    }

    [Fact]
    public async Task IsUsernameInUseAsync_WithExistingUsername_ShouldReturnTrue()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var existingPlayer = players.First();

        // Act
        var result = await _playerRepository.IsUsernameInUseAsync(
            existingPlayer.Username, existingPlayer.CampaignId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsUsernameInUseAsync_WithNewUsername_ShouldReturnFalse()
    {
        // Act
        var result = await _playerRepository.IsUsernameInUseAsync("NewUniqueUsername", 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsEmailInUseAsync_WithExistingEmail_ShouldReturnTrue()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var existingPlayer = players.First();

        // Act
        var result = await _playerRepository.IsEmailInUseAsync(
            existingPlayer.Email, existingPlayer.CampaignId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailInUseAsync_WithNewEmail_ShouldReturnFalse()
    {
        // Act
        var result = await _playerRepository.IsEmailInUseAsync("newemail@test.com", 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var newPlayer = new RpgPlayer
        {
            Username = "PlayerToDelete",
            Email = "delete@test.com",
            CampaignId = 1
        };
        var created = await _playerRepository.CreateAsync(newPlayer);

        // Act
        var result = await _playerRepository.DeleteAsync(created.Id);

        // Assert
        Assert.True(result);
        
        // Vérifier que le joueur n'existe plus
        var deleted = await _playerRepository.GetByIdAsync(created.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task ExistsAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var players = await _playerRepository.GetAllAsync();
        var existingPlayer = players.First();

        // Act
        var result = await _playerRepository.ExistsAsync(existingPlayer.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _playerRepository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }
}