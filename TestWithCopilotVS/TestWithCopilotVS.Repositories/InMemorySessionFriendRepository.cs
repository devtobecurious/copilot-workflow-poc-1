using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des participants de session.
/// </summary>
public class InMemorySessionFriendRepository : ISessionFriendRepository
{
    private readonly List<SessionFriend> _sessionFriends = new();
    private int _nextId = 1;

    public InMemorySessionFriendRepository()
    {
        InitializeData();
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeData()
    {
        var sessionFriend1 = new SessionFriend
        {
            Id = _nextId++,
            SessionId = 1,
            FriendId = 1,
            Status = FriendSessionStatus.Primary,
            JoinedAt = DateTime.UtcNow.AddHours(-2),
            IsActive = true
        };

        var sessionFriend2 = new SessionFriend
        {
            Id = _nextId++,
            SessionId = 1,
            FriendId = 2,
            Status = FriendSessionStatus.Primary,
            JoinedAt = DateTime.UtcNow.AddHours(-2),
            IsActive = true
        };

        _sessionFriends.AddRange(new[] { sessionFriend1, sessionFriend2 });
    }

    public Task<IEnumerable<SessionFriend>> GetBySessionAsync(int sessionId)
    {
        var participants = _sessionFriends.Where(sf => sf.SessionId == sessionId);
        return Task.FromResult(participants);
    }

    public Task<IEnumerable<SessionFriend>> GetByFriendAsync(int friendId)
    {
        var participations = _sessionFriends.Where(sf => sf.FriendId == friendId);
        return Task.FromResult(participations);
    }

    public Task<SessionFriend?> GetBySessionAndFriendAsync(int sessionId, int friendId)
    {
        var sessionFriend = _sessionFriends.FirstOrDefault(sf => 
            sf.SessionId == sessionId && sf.FriendId == friendId);
        return Task.FromResult(sessionFriend);
    }

    public Task<SessionFriend> AddAsync(SessionFriend sessionFriend)
    {
        if (sessionFriend == null)
            throw new ArgumentNullException(nameof(sessionFriend));

        sessionFriend.Id = _nextId++;
        sessionFriend.JoinedAt = DateTime.UtcNow;
        _sessionFriends.Add(sessionFriend);

        return Task.FromResult(sessionFriend);
    }

    public Task<SessionFriend?> UpdateStatusAsync(int sessionId, int friendId, FriendSessionStatus status)
    {
        var sessionFriend = _sessionFriends.FirstOrDefault(sf => 
            sf.SessionId == sessionId && sf.FriendId == friendId);

        if (sessionFriend == null)
            return Task.FromResult<SessionFriend?>(null);

        sessionFriend.Status = status;
        return Task.FromResult<SessionFriend?>(sessionFriend);
    }

    public Task<bool> RemoveFromSessionAsync(int sessionId, int friendId)
    {
        var sessionFriend = _sessionFriends.FirstOrDefault(sf => 
            sf.SessionId == sessionId && sf.FriendId == friendId);

        if (sessionFriend == null)
            return Task.FromResult(false);

        sessionFriend.IsActive = false;
        return Task.FromResult(true);
    }

    public Task<bool> IsParticipatingAsync(int sessionId, int friendId)
    {
        var isParticipating = _sessionFriends.Any(sf => 
            sf.SessionId == sessionId && sf.FriendId == friendId && sf.IsActive);
        return Task.FromResult(isParticipating);
    }

    public Task<int> CountActiveParticipantsAsync(int sessionId)
    {
        var count = _sessionFriends.Count(sf => sf.SessionId == sessionId && sf.IsActive);
        return Task.FromResult(count);
    }

    public Task<IEnumerable<SessionFriend>> GetBySessionAndStatusAsync(int sessionId, FriendSessionStatus status)
    {
        var participants = _sessionFriends.Where(sf => 
            sf.SessionId == sessionId && sf.Status == status);
        return Task.FromResult(participants);
    }
}