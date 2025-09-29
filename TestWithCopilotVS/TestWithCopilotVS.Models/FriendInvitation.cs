using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente une invitation à rejoindre une session de jeu.
/// </summary>
public class FriendInvitation
{
    /// <summary>
    /// Identifiant unique de l'invitation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant de la session de jeu.
    /// </summary>
    [Required]
    public int SessionId { get; set; }

    /// <summary>
    /// Identifiant de l'ami invité.
    /// </summary>
    [Required]
    public int FriendId { get; set; }

    /// <summary>
    /// Identifiant de l'ami qui a envoyé l'invitation.
    /// </summary>
    [Required]
    public int InvitedById { get; set; }

    /// <summary>
    /// Date et heure de création de l'invitation.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date et heure d'expiration de l'invitation.
    /// </summary>
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);

    /// <summary>
    /// Statut de l'invitation.
    /// </summary>
    [Required]
    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

    /// <summary>
    /// Date et heure de réponse à l'invitation.
    /// </summary>
    public DateTime? RespondedAt { get; set; }

    /// <summary>
    /// Message personnalisé de l'invitation (optionnel).
    /// </summary>
    [StringLength(500)]
    public string? Message { get; set; }

    /// <summary>
    /// Référence vers la session de jeu.
    /// </summary>
    public GameSession? Session { get; set; }

    /// <summary>
    /// Référence vers l'ami invité.
    /// </summary>
    public Friend? InvitedFriend { get; set; }

    /// <summary>
    /// Référence vers l'ami qui a envoyé l'invitation.
    /// </summary>
    public Friend? InvitedBy { get; set; }
}

/// <summary>
/// Énumération des statuts possibles d'une invitation.
/// </summary>
public enum InvitationStatus
{
    /// <summary>
    /// Invitation en attente de réponse.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Invitation acceptée.
    /// </summary>
    Accepted = 1,

    /// <summary>
    /// Invitation refusée.
    /// </summary>
    Declined = 2,

    /// <summary>
    /// Invitation expirée.
    /// </summary>
    Expired = 3,

    /// <summary>
    /// Invitation annulée par l'expéditeur.
    /// </summary>
    Cancelled = 4
}