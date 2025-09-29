using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;
using Xunit;

namespace TestWithCopilotVS.Tests;

/// <summary>
/// Tests unitaires pour le repository des sessions de jeu.
/// </summary>
public class GameSessionRepositoryTests
{
    private readonly IFriendRepository _friendRepository;
    private readonly IGameSessionRepository _sessionRepository;

    public GameSessionRepositoryTests()
    {
        _friendRepository = new InMemoryFriendRepository();
        _sessionRepository = new InMemoryGameSessionRepository(_friendRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSessions()
    {
        // Act
        var sessions = await _sessionRepository.GetAllAsync();

        // Assert
        Assert.NotNull(sessions);
        Assert.NotEmpty(sessions);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnSession()
    {
        // Arrange
        var sessions = await _sessionRepository.GetAllAsync();
        var existingSession = sessions.First();

        // Act
        var result = await _sessionRepository.GetByIdAsync(existingSession.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingSession.Id, result.Id);
        Assert.Equal(existingSession.Name, result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _sessionRepository.GetByIdAsync(99999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidSession_ShouldCreateAndReturnSession()
    {
        // Arrange
        var newSession = new GameSession
        {
            Name = "Test Session",
            CreatorId = 1,
            IsActive = true
        };

        // Act
        var result = await _sessionRepository.CreateAsync(newSession);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Test Session", result.Name);
        Assert.Equal(1, result.CreatorId);
        Assert.True(result.IsActive);
        Assert.True(result.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task CreateAsync_WithNullSession_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sessionRepository.CreateAsync(null!));
    }

    [Fact]
    public async Task GetActiveSessionsAsync_ShouldReturnOnlyActiveSessions()
    {
        // Arrange
        var activeSession = new GameSession
        {
            Name = "Active Session",
            CreatorId = 1,
            IsActive = true
        };
        var inactiveSession = new GameSession
        {
            Name = "Inactive Session",
            CreatorId = 1,
            IsActive = false
        };

        await _sessionRepository.CreateAsync(activeSession);
        await _sessionRepository.CreateAsync(inactiveSession);

        // Act
        var activeSessions = await _sessionRepository.GetActiveSessionsAsync();

        // Assert
        Assert.NotNull(activeSessions);
        Assert.All(activeSessions, session => Assert.True(session.IsActive));
    }

    [Fact]
    public async Task GetByCreatorAsync_ShouldReturnCreatorSessions()
    {
        // Arrange
        var creatorId = 1;
        var session1 = new GameSession { Name = "Session 1", CreatorId = creatorId };
        var session2 = new GameSession { Name = "Session 2", CreatorId = creatorId };
        var session3 = new GameSession { Name = "Session 3", CreatorId = 2 };

        await _sessionRepository.CreateAsync(session1);
        await _sessionRepository.CreateAsync(session2);
        await _sessionRepository.CreateAsync(session3);

        // Act
        var creatorSessions = await _sessionRepository.GetByCreatorAsync(creatorId);

        // Assert
        Assert.NotNull(creatorSessions);
        Assert.All(creatorSessions, session => Assert.Equal(creatorId, session.CreatorId));
        Assert.True(creatorSessions.Count() >= 2);
    }

    [Fact]
    public async Task UpdateAsync_WithValidSession_ShouldUpdateSession()
    {
        // Arrange
        var session = new GameSession
        {
            Name = "Original Name",
            CreatorId = 1,
            IsActive = true
        };
        var createdSession = await _sessionRepository.CreateAsync(session);
        
        createdSession.Name = "Updated Name";
        createdSession.IsActive = false;

        // Act
        var result = await _sessionRepository.UpdateAsync(createdSession);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Name", result.Name);
        Assert.False(result.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistentSession_ShouldReturnNull()
    {
        // Arrange
        var nonExistentSession = new GameSession
        {
            Id = 99999,
            Name = "Non-existent",
            CreatorId = 1
        };

        // Act
        var result = await _sessionRepository.UpdateAsync(nonExistentSession);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task EndSessionAsync_WithActiveSession_ShouldEndSession()
    {
        // Arrange
        var session = new GameSession
        {
            Name = "Active Session",
            CreatorId = 1,
            IsActive = true
        };
        var createdSession = await _sessionRepository.CreateAsync(session);

        // Act
        var result = await _sessionRepository.EndSessionAsync(createdSession.Id);

        // Assert
        Assert.True(result);
        
        var endedSession = await _sessionRepository.GetByIdAsync(createdSession.Id);
        Assert.NotNull(endedSession);
        Assert.False(endedSession.IsActive);
        Assert.NotNull(endedSession.EndedAt);
    }

    [Fact]
    public async Task EndSessionAsync_WithNonExistentSession_ShouldReturnFalse()
    {
        // Act
        var result = await _sessionRepository.EndSessionAsync(99999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingSession_ShouldDeleteSession()
    {
        // Arrange
        var session = new GameSession
        {
            Name = "To Delete",
            CreatorId = 1
        };
        var createdSession = await _sessionRepository.CreateAsync(session);

        // Act
        var result = await _sessionRepository.DeleteAsync(createdSession.Id);

        // Assert
        Assert.True(result);
        
        var deletedSession = await _sessionRepository.GetByIdAsync(createdSession.Id);
        Assert.Null(deletedSession);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistentSession_ShouldReturnFalse()
    {
        // Act
        var result = await _sessionRepository.DeleteAsync(99999);

        // Assert
        Assert.False(result);
    }
}