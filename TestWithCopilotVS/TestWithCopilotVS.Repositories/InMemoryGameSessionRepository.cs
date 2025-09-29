using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des sessions de jeu.
/// </summary>
public class InMemoryGameSessionRepository : IGameSessionRepository
{
    private readonly List<GameSession> _sessions = new();
    private readonly IFriendRepository _friendRepository;
    private int _nextId = 1;

    public InMemoryGameSessionRepository(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository ?? throw new ArgumentNullException(nameof(friendRepository));
        InitializeData();
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeData()
    {
        var session1 = new GameSession
        {
            Id = _nextId++,
            Name = "Session test 1",
            CreatorId = 1,
            CreatedAt = DateTime.UtcNow.AddHours(-2),
            IsActive = true
        };

        var session2 = new GameSession
        {
            Id = _nextId++,
            Name = "Session terminée",
            CreatorId = 2,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            IsActive = false,
            EndedAt = DateTime.UtcNow.AddHours(-1)
        };

        _sessions.AddRange(new[] { session1, session2 });
    }

    public Task<IEnumerable<GameSession>> GetAllAsync()
    {
        return Task.FromResult(_sessions.AsEnumerable());
    }

    public Task<GameSession?> GetByIdAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(session);
    }

    public Task<IEnumerable<GameSession>> GetActiveSessionsAsync()
    {
        var activeSessions = _sessions.Where(s => s.IsActive);
        return Task.FromResult(activeSessions);
    }

    public Task<IEnumerable<GameSession>> GetByCreatorAsync(int creatorId)
    {
        var creatorSessions = _sessions.Where(s => s.CreatorId == creatorId);
        return Task.FromResult(creatorSessions);
    }

    public Task<IEnumerable<GameSession>> GetByParticipantAsync(int friendId)
    {
        var participantSessions = _sessions.Where(s => 
            s.Participants.Any(p => p.FriendId == friendId && p.IsActive));
        return Task.FromResult(participantSessions);
    }

    public Task<GameSession> CreateAsync(GameSession session)
    {
        if (session == null)
            throw new ArgumentNullException(nameof(session));

        session.Id = _nextId++;
        session.CreatedAt = DateTime.UtcNow;
        _sessions.Add(session);

        return Task.FromResult(session);
    }

    public Task<GameSession?> UpdateAsync(GameSession session)
    {
        if (session == null)
            return Task.FromResult<GameSession?>(null);

        var existingSession = _sessions.FirstOrDefault(s => s.Id == session.Id);
        if (existingSession == null)
            return Task.FromResult<GameSession?>(null);

        var index = _sessions.IndexOf(existingSession);
        _sessions[index] = session;

        return Task.FromResult<GameSession?>(session);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        if (session == null)
            return Task.FromResult(false);

        _sessions.Remove(session);
        return Task.FromResult(true);
    }

    public Task<bool> EndSessionAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        if (session == null || !session.IsActive)
            return Task.FromResult(false);

        session.IsActive = false;
        session.EndedAt = DateTime.UtcNow;
        return Task.FromResult(true);
    }
}