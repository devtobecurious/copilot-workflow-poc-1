using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente une session de jeu de rôle au sein d'une campagne.
/// </summary>
public class RpgSession
{
    /// <summary>
    /// Identifiant unique de la session.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre ou nom de la session.
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Résumé de ce qui s'est passé pendant la session.
    /// </summary>
    [StringLength(5000)]
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant de la campagne à laquelle appartient cette session.
    /// </summary>
    [Required]
    public int CampaignId { get; set; }

    /// <summary>
    /// Date et heure de début de la session.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Date et heure de fin de la session.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Durée prévue de la session en minutes.
    /// </summary>
    [Range(30, 600)] // Entre 30 minutes et 10 heures
    public int PlannedDurationMinutes { get; set; } = 240; // 4 heures par défaut

    /// <summary>
    /// Statut de la session.
    /// </summary>
    [Required]
    public RpgSessionStatus Status { get; set; } = RpgSessionStatus.Planned;

    /// <summary>
    /// Identifiant du maître de jeu pour cette session.
    /// </summary>
    [Required]
    public int GameMasterId { get; set; }

    /// <summary>
    /// Notes du maître de jeu pour cette session.
    /// </summary>
    [StringLength(10000)]
    public string GameMasterNotes { get; set; } = string.Empty;

    /// <summary>
    /// Liste des joueurs présents à cette session.
    /// </summary>
    public List<RpgSessionParticipant> Participants { get; set; } = new();

    /// <summary>
    /// Événements marquants de la session.
    /// </summary>
    public List<RpgSessionEvent> Events { get; set; } = new();
}

/// <summary>
/// Statut possible d'une session de jeu de rôle.
/// </summary>
public enum RpgSessionStatus
{
    /// <summary>
    /// Session planifiée mais pas encore commencée.
    /// </summary>
    Planned,

    /// <summary>
    /// Session en cours.
    /// </summary>
    InProgress,

    /// <summary>
    /// Session terminée normalement.
    /// </summary>
    Completed,

    /// <summary>
    /// Session annulée.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Session reportée à une date ultérieure.
    /// </summary>
    Postponed
}