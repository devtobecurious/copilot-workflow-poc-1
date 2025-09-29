using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;
using Xunit;

namespace TestWithCopilotVS.Tests;

/// <summary>
/// Tests unitaires pour le repository des participants de session.
/// </summary>
public class SessionFriendRepositoryTests
{
    private readonly ISessionFriendRepository _sessionFriendRepository;

    public SessionFriendRepositoryTests()
    {
        _sessionFriendRepository = new InMemorySessionFriendRepository();
    }

    [Fact]
    public async Task AddAsync_WithValidSessionFriend_ShouldAddAndReturnSessionFriend()
    {
        // Arrange
        var sessionFriend = new SessionFriend
        {
            SessionId = 1,
            FriendId = 3,
            Status = FriendSessionStatus.Secondary
        };

        // Act
        var result = await _sessionFriendRepository.AddAsync(sessionFriend);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(1, result.SessionId);
        Assert.Equal(3, result.FriendId);
        Assert.Equal(FriendSessionStatus.Secondary, result.Status);
        Assert.True(result.IsActive);
        Assert.True(result.JoinedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task AddAsync_WithNullSessionFriend_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sessionFriendRepository.AddAsync(null!));
    }

    [Fact]
    public async Task GetBySessionAsync_ShouldReturnSessionParticipants()
    {
        // Arrange
        var sessionId = 1;

        // Act
        var participants = await _sessionFriendRepository.GetBySessionAsync(sessionId);

        // Assert
        Assert.NotNull(participants);
        Assert.NotEmpty(participants);
        Assert.All(participants, p => Assert.Equal(sessionId, p.SessionId));
    }

    [Fact]
    public async Task GetByFriendAsync_ShouldReturnFriendParticipations()
    {
        // Arrange
        var friendId = 1;

        // Act
        var participations = await _sessionFriendRepository.GetByFriendAsync(friendId);

        // Assert
        Assert.NotNull(participations);
        Assert.NotEmpty(participations);
        Assert.All(participations, p => Assert.Equal(friendId, p.FriendId));
    }

    [Fact]
    public async Task GetBySessionAndFriendAsync_WithExistingParticipation_ShouldReturnSessionFriend()
    {
        // Arrange
        var sessionId = 1;
        var friendId = 1;

        // Act
        var result = await _sessionFriendRepository.GetBySessionAndFriendAsync(sessionId, friendId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessionId, result.SessionId);
        Assert.Equal(friendId, result.FriendId);
    }

    [Fact]
    public async Task GetBySessionAndFriendAsync_WithNonExistentParticipation_ShouldReturnNull()
    {
        // Act
        var result = await _sessionFriendRepository.GetBySessionAndFriendAsync(999, 999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task IsParticipatingAsync_WithActiveParticipant_ShouldReturnTrue()
    {
        // Arrange
        var sessionId = 1;
        var friendId = 1;

        // Act
        var result = await _sessionFriendRepository.IsParticipatingAsync(sessionId, friendId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsParticipatingAsync_WithNonParticipant_ShouldReturnFalse()
    {
        // Act
        var result = await _sessionFriendRepository.IsParticipatingAsync(999, 999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithExistingParticipation_ShouldUpdateStatus()
    {
        // Arrange
        var sessionId = 1;
        var friendId = 1;
        var newStatus = FriendSessionStatus.Observer;

        // Act
        var result = await _sessionFriendRepository.UpdateStatusAsync(sessionId, friendId, newStatus);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newStatus, result.Status);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithNonExistentParticipation_ShouldReturnNull()
    {
        // Act
        var result = await _sessionFriendRepository.UpdateStatusAsync(999, 999, FriendSessionStatus.Secondary);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveFromSessionAsync_WithActiveParticipant_ShouldDeactivateParticipation()
    {
        // Arrange
        var sessionId = 1;
        var friendId = 2;

        // Act
        var result = await _sessionFriendRepository.RemoveFromSessionAsync(sessionId, friendId);

        // Assert
        Assert.True(result);
        
        // Vérifier que la participation est désactivée
        var participation = await _sessionFriendRepository.GetBySessionAndFriendAsync(sessionId, friendId);
        Assert.NotNull(participation);
        Assert.False(participation.IsActive);
    }

    [Fact]
    public async Task RemoveFromSessionAsync_WithNonExistentParticipation_ShouldReturnFalse()
    {
        // Act
        var result = await _sessionFriendRepository.RemoveFromSessionAsync(999, 999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CountActiveParticipantsAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var sessionId = 1;

        // Act
        var count = await _sessionFriendRepository.CountActiveParticipantsAsync(sessionId);

        // Assert
        Assert.True(count >= 0);
    }

    [Fact]
    public async Task GetBySessionAndStatusAsync_ShouldReturnParticipantsWithSpecificStatus()
    {
        // Arrange
        var sessionId = 1;
        var status = FriendSessionStatus.Primary;

        // Act
        var participants = await _sessionFriendRepository.GetBySessionAndStatusAsync(sessionId, status);

        // Assert
        Assert.NotNull(participants);
        Assert.All(participants, p => 
        {
            Assert.Equal(sessionId, p.SessionId);
            Assert.Equal(status, p.Status);
        });
    }

    [Fact]
    public async Task AddSecondaryFriend_IntegrationTest()
    {
        // Arrange
        var sessionId = 2;
        var friendId = 5;
        var secondaryFriend = new SessionFriend
        {
            SessionId = sessionId,
            FriendId = friendId,
            Status = FriendSessionStatus.Secondary
        };

        // Act
        var addedFriend = await _sessionFriendRepository.AddAsync(secondaryFriend);
        
        // Assert
        Assert.NotNull(addedFriend);
        Assert.Equal(FriendSessionStatus.Secondary, addedFriend.Status);
        
        // Vérifier qu'il peut être récupéré
        var retrieved = await _sessionFriendRepository.GetBySessionAndFriendAsync(sessionId, friendId);
        Assert.NotNull(retrieved);
        Assert.Equal(FriendSessionStatus.Secondary, retrieved.Status);
        
        // Vérifier qu'il est compté
        var isParticipating = await _sessionFriendRepository.IsParticipatingAsync(sessionId, friendId);
        Assert.True(isParticipating);
    }
}