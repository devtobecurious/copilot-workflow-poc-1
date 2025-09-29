using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente la relation entre un ami et une session de jeu avec son statut.
/// </summary>
public class SessionFriend
{
    /// <summary>
    /// Identifiant unique de la relation session-ami.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant de la session de jeu.
    /// </summary>
    [Required]
    public int SessionId { get; set; }

    /// <summary>
    /// Identifiant de l'ami participant.
    /// </summary>
    [Required]
    public int FriendId { get; set; }

    /// <summary>
    /// Statut de l'ami dans la session.
    /// </summary>
    [Required]
    public FriendSessionStatus Status { get; set; } = FriendSessionStatus.Primary;

    /// <summary>
    /// Date et heure d'ajout de l'ami à la session.
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indique si l'ami est actif dans la session.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Référence vers la session de jeu.
    /// </summary>
    public GameSession? Session { get; set; }

    /// <summary>
    /// Référence vers l'ami.
    /// </summary>
    public Friend? Friend { get; set; }
}

/// <summary>
/// Énumération des statuts possibles d'un ami dans une session.
/// </summary>
public enum FriendSessionStatus
{
    /// <summary>
    /// Ami primaire (présent dès le début de la session).
    /// </summary>
    Primary = 0,

    /// <summary>
    /// Ami secondaire (ajouté en cours de session).
    /// </summary>
    Secondary = 1,

    /// <summary>
    /// Spectateur (peut observer mais ne participe pas).
    /// </summary>
    Observer = 2,

    /// <summary>
    /// Invité en attente de confirmation.
    /// </summary>
    Pending = 3
}