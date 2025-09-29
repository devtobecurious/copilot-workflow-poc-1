using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente la participation d'un joueur à une session de jeu de rôle.
/// </summary>
public class RpgSessionParticipant
{
    /// <summary>
    /// Identifiant unique du participant.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant de la session.
    /// </summary>
    [Required]
    public int SessionId { get; set; }

    /// <summary>
    /// Identifiant du joueur.
    /// </summary>
    [Required]
    public int PlayerId { get; set; }

    /// <summary>
    /// Identifiant du personnage utilisé dans cette session (optionnel).
    /// </summary>
    public int? CharacterId { get; set; }

    /// <summary>
    /// Statut de la participation du joueur.
    /// </summary>
    [Required]
    public RpgParticipationStatus Status { get; set; } = RpgParticipationStatus.Confirmed;

    /// <summary>
    /// Heure d'arrivée du joueur à la session.
    /// </summary>
    public DateTime? ArrivalTime { get; set; }

    /// <summary>
    /// Heure de départ du joueur de la session.
    /// </summary>
    public DateTime? DepartureTime { get; set; }

    /// <summary>
    /// Indique si le joueur était présent du début à la fin.
    /// </summary>
    public bool WasFullyPresent { get; set; } = true;

    /// <summary>
    /// Points d'expérience gagnés pendant cette session.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int ExperienceGained { get; set; } = 0;

    /// <summary>
    /// Notes du joueur sur cette session.
    /// </summary>
    [StringLength(2000)]
    public string PlayerNotes { get; set; } = string.Empty;

    /// <summary>
    /// Notes du MJ sur la participation de ce joueur.
    /// </summary>
    [StringLength(2000)]
    public string GameMasterNotes { get; set; } = string.Empty;

    /// <summary>
    /// Évaluation du roleplay du joueur (1-5).
    /// </summary>
    [Range(1, 5)]
    public int? RoleplayRating { get; set; }

    /// <summary>
    /// Date de création de l'enregistrement.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Statut de participation d'un joueur à une session.
/// </summary>
public enum RpgParticipationStatus
{
    /// <summary>
    /// Participation confirmée.
    /// </summary>
    Confirmed,

    /// <summary>
    /// En attente de confirmation.
    /// </summary>
    Pending,

    /// <summary>
    /// Joueur absent.
    /// </summary>
    Absent,

    /// <summary>
    /// Participation annulée.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Arrivée en retard.
    /// </summary>
    Late,

    /// <summary>
    /// Départ anticipé.
    /// </summary>
    LeftEarly
}