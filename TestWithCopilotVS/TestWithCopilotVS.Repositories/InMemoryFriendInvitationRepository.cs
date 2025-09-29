using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;

namespace TestWithCopilotVS.Repositories;

/// <summary>
/// Implémentation en mémoire du repository des invitations d'amis.
/// </summary>
public class InMemoryFriendInvitationRepository : IFriendInvitationRepository
{
    private readonly List<FriendInvitation> _invitations = new();
    private int _nextId = 1;

    public Task<IEnumerable<FriendInvitation>> GetAllAsync()
    {
        return Task.FromResult(_invitations.AsEnumerable());
    }

    public Task<FriendInvitation?> GetByIdAsync(int id)
    {
        var invitation = _invitations.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(invitation);
    }

    public Task<IEnumerable<FriendInvitation>> GetBySessionAsync(int sessionId)
    {
        var sessionInvitations = _invitations.Where(i => i.SessionId == sessionId);
        return Task.FromResult(sessionInvitations);
    }

    public Task<IEnumerable<FriendInvitation>> GetByInvitedFriendAsync(int friendId)
    {
        var receivedInvitations = _invitations.Where(i => i.FriendId == friendId);
        return Task.FromResult(receivedInvitations);
    }

    public Task<IEnumerable<FriendInvitation>> GetByInviterAsync(int invitedById)
    {
        var sentInvitations = _invitations.Where(i => i.InvitedById == invitedById);
        return Task.FromResult(sentInvitations);
    }

    public Task<IEnumerable<FriendInvitation>> GetPendingInvitationsAsync(int friendId)
    {
        var pendingInvitations = _invitations.Where(i => 
            i.FriendId == friendId && 
            i.Status == InvitationStatus.Pending && 
            i.ExpiresAt > DateTime.UtcNow);
        return Task.FromResult(pendingInvitations);
    }

    public Task<FriendInvitation> CreateAsync(FriendInvitation invitation)
    {
        if (invitation == null)
            throw new ArgumentNullException(nameof(invitation));

        invitation.Id = _nextId++;
        invitation.CreatedAt = DateTime.UtcNow;
        
        // Définir la date d'expiration si elle n'est pas définie
        if (invitation.ExpiresAt == default)
            invitation.ExpiresAt = DateTime.UtcNow.AddHours(24);

        _invitations.Add(invitation);
        return Task.FromResult(invitation);
    }

    public Task<FriendInvitation?> UpdateAsync(FriendInvitation invitation)
    {
        if (invitation == null)
            return Task.FromResult<FriendInvitation?>(null);

        var existingInvitation = _invitations.FirstOrDefault(i => i.Id == invitation.Id);
        if (existingInvitation == null)
            return Task.FromResult<FriendInvitation?>(null);

        var index = _invitations.IndexOf(existingInvitation);
        _invitations[index] = invitation;

        return Task.FromResult<FriendInvitation?>(invitation);
    }

    public Task<FriendInvitation?> RespondToInvitationAsync(int id, InvitationStatus status)
    {
        var invitation = _invitations.FirstOrDefault(i => i.Id == id);
        if (invitation == null || invitation.Status != InvitationStatus.Pending)
            return Task.FromResult<FriendInvitation?>(null);

        // Vérifier que le statut est valide pour une réponse
        if (status != InvitationStatus.Accepted && status != InvitationStatus.Declined)
            return Task.FromResult<FriendInvitation?>(null);

        invitation.Status = status;
        invitation.RespondedAt = DateTime.UtcNow;

        return Task.FromResult<FriendInvitation?>(invitation);
    }

    public Task<bool> CancelInvitationAsync(int id)
    {
        var invitation = _invitations.FirstOrDefault(i => i.Id == id);
        if (invitation == null || invitation.Status != InvitationStatus.Pending)
            return Task.FromResult(false);

        invitation.Status = InvitationStatus.Cancelled;
        invitation.RespondedAt = DateTime.UtcNow;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var invitation = _invitations.FirstOrDefault(i => i.Id == id);
        if (invitation == null)
            return Task.FromResult(false);

        _invitations.Remove(invitation);
        return Task.FromResult(true);
    }

    public Task<int> MarkExpiredInvitationsAsync()
    {
        var expiredInvitations = _invitations.Where(i => 
            i.Status == InvitationStatus.Pending && 
            i.ExpiresAt <= DateTime.UtcNow).ToList();

        foreach (var invitation in expiredInvitations)
        {
            invitation.Status = InvitationStatus.Expired;
            invitation.RespondedAt = DateTime.UtcNow;
        }

        return Task.FromResult(expiredInvitations.Count);
    }

    public Task<bool> HasPendingInvitationAsync(int sessionId, int friendId)
    {
        var hasPending = _invitations.Any(i => 
            i.SessionId == sessionId && 
            i.FriendId == friendId && 
            i.Status == InvitationStatus.Pending && 
            i.ExpiresAt > DateTime.UtcNow);

        return Task.FromResult(hasPending);
    }
}