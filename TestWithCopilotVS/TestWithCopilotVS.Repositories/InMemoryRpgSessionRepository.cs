using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository pour les sessions RPG.
/// </summary>
public class InMemoryRpgSessionRepository : IRpgSessionRepository
{
    private readonly List<RpgSession> _sessions = new();
    private int _nextId = 1;

    public InMemoryRpgSessionRepository()
    {
        InitializeTestData();
    }

    public Task<IEnumerable<RpgSession>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<RpgSession>>(_sessions.ToList());
    }

    public Task<RpgSession?> GetByIdAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(session);
    }

    public Task<IEnumerable<RpgSession>> GetByCampaignAsync(int campaignId)
    {
        var sessions = _sessions.Where(s => s.CampaignId == campaignId)
                               .OrderBy(s => s.StartTime).ToList();
        return Task.FromResult<IEnumerable<RpgSession>>(sessions);
    }

    public Task<IEnumerable<RpgSession>> GetByStatusAsync(RpgSessionStatus status, int? campaignId = null)
    {
        var query = _sessions.Where(s => s.Status == status);
        
        if (campaignId.HasValue)
            query = query.Where(s => s.CampaignId == campaignId.Value);

        return Task.FromResult<IEnumerable<RpgSession>>(query.ToList());
    }

    public Task<IEnumerable<RpgSession>> GetPlannedSessionsAsync(int campaignId)
    {
        var sessions = _sessions.Where(s => s.CampaignId == campaignId && s.Status == RpgSessionStatus.Planned)
                               .OrderBy(s => s.StartTime).ToList();
        return Task.FromResult<IEnumerable<RpgSession>>(sessions);
    }

    public Task<IEnumerable<RpgSession>> GetActiveSessionsAsync()
    {
        var sessions = _sessions.Where(s => s.Status == RpgSessionStatus.InProgress).ToList();
        return Task.FromResult<IEnumerable<RpgSession>>(sessions);
    }

    public Task<IEnumerable<RpgSession>> GetByGameMasterAsync(int gameMasterId)
    {
        var sessions = _sessions.Where(s => s.GameMasterId == gameMasterId)
                               .OrderByDescending(s => s.StartTime).ToList();
        return Task.FromResult<IEnumerable<RpgSession>>(sessions);
    }

    public Task<IEnumerable<RpgSession>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, int? campaignId = null)
    {
        var query = _sessions.Where(s => s.StartTime >= startDate && s.StartTime <= endDate);
        
        if (campaignId.HasValue)
            query = query.Where(s => s.CampaignId == campaignId.Value);

        return Task.FromResult<IEnumerable<RpgSession>>(query.OrderBy(s => s.StartTime).ToList());
    }

    public Task<RpgSession?> GetLastCompletedSessionAsync(int campaignId)
    {
        var session = _sessions.Where(s => s.CampaignId == campaignId && s.Status == RpgSessionStatus.Completed)
                              .OrderByDescending(s => s.EndTime ?? s.StartTime)
                              .FirstOrDefault();
        return Task.FromResult(session);
    }

    public Task<RpgSession?> GetNextPlannedSessionAsync(int campaignId)
    {
        var session = _sessions.Where(s => s.CampaignId == campaignId && s.Status == RpgSessionStatus.Planned)
                              .OrderBy(s => s.StartTime)
                              .FirstOrDefault();
        return Task.FromResult(session);
    }

    public Task<RpgSession> CreateAsync(RpgSession session)
    {
        session.Id = _nextId++;
        _sessions.Add(session);
        return Task.FromResult(session);
    }

    public Task<RpgSession?> UpdateAsync(RpgSession session)
    {
        var existingSession = _sessions.FirstOrDefault(s => s.Id == session.Id);
        if (existingSession == null)
            return Task.FromResult<RpgSession?>(null);

        // Mise à jour des propriétés
        existingSession.Title = session.Title;
        existingSession.Summary = session.Summary;
        existingSession.StartTime = session.StartTime;
        existingSession.EndTime = session.EndTime;
        existingSession.PlannedDurationMinutes = session.PlannedDurationMinutes;
        existingSession.Status = session.Status;
        existingSession.GameMasterNotes = session.GameMasterNotes;

        return Task.FromResult<RpgSession?>(existingSession);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        if (session == null)
            return Task.FromResult(false);

        _sessions.Remove(session);
        return Task.FromResult(true);
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _sessions.Any(s => s.Id == id);
        return Task.FromResult(exists);
    }

    public Task<bool> UpdateStatusAsync(int sessionId, RpgSessionStatus status)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
            return Task.FromResult(false);

        session.Status = status;
        return Task.FromResult(true);
    }

    public Task<bool> StartSessionAsync(int sessionId)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null || session.Status != RpgSessionStatus.Planned)
            return Task.FromResult(false);

        session.Status = RpgSessionStatus.InProgress;
        session.StartTime = DateTime.UtcNow;
        return Task.FromResult(true);
    }

    public Task<bool> CompleteSessionAsync(int sessionId, string? summary = null)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null || session.Status != RpgSessionStatus.InProgress)
            return Task.FromResult(false);

        session.Status = RpgSessionStatus.Completed;
        session.EndTime = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(summary))
            session.Summary = summary;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Initialise des données de test.
    /// </summary>
    private void InitializeTestData()
    {
        var now = DateTime.UtcNow;
        var sessions = new List<RpgSession>
        {
            new RpgSession
            {
                Id = _nextId++,
                Title = "Première Aventure - L'Auberge du Gobelin qui Rit",
                Summary = "Les héros se rencontrent dans une auberge et acceptent leur première mission.",
                CampaignId = 1,
                GameMasterId = 1,
                StartTime = now.AddDays(-7),
                EndTime = now.AddDays(-7).AddHours(4),
                PlannedDurationMinutes = 240,
                Status = RpgSessionStatus.Completed,
                GameMasterNotes = "Session d'introduction réussie. Les joueurs ont bien intégré leurs personnages."
            },
            new RpgSession
            {
                Id = _nextId++,
                Title = "Les Caves Mystérieuses",
                Summary = "Exploration des caves sous la ville à la recherche d'artefacts anciens.",
                CampaignId = 1,
                GameMasterId = 1,
                StartTime = now.AddDays(-3),
                EndTime = now.AddDays(-3).AddHours(3.5),
                PlannedDurationMinutes = 240,
                Status = RpgSessionStatus.Completed,
                GameMasterNotes = "Combat épique contre le minotaure gardien. Bonne coopération d'équipe."
            },
            new RpgSession
            {
                Id = _nextId++,
                Title = "La Forteresse Assiégée",
                Summary = "",
                CampaignId = 1,
                GameMasterId = 1,
                StartTime = now.AddDays(3),
                PlannedDurationMinutes = 300,
                Status = RpgSessionStatus.Planned,
                GameMasterNotes = "Préparer la bataille pour la libération de la forteresse. Prévoir des figurines pour le combat tactique."
            },
            new RpgSession
            {
                Id = _nextId++,
                Title = "L'Éveil des Anciens - Prologue",
                Summary = "",
                CampaignId = 2,
                GameMasterId = 2,
                StartTime = now.AddDays(14),
                PlannedDurationMinutes = 240,
                Status = RpgSessionStatus.Planned,
                GameMasterNotes = "Session de création de personnages et introduction au monde de Pathfinder."
            }
        };

        _sessions.AddRange(sessions);
    }
}