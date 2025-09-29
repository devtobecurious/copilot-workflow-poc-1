using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un événement marquant survenu pendant une session de jeu de rôle.
/// </summary>
public class RpgSessionEvent
{
    /// <summary>
    /// Identifiant unique de l'événement.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant de la session à laquelle appartient cet événement.
    /// </summary>
    [Required]
    public int SessionId { get; set; }

    /// <summary>
    /// Titre ou nom de l'événement.
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description détaillée de l'événement.
    /// </summary>
    [Required]
    [StringLength(5000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Type d'événement.
    /// </summary>
    [Required]
    public RpgEventType EventType { get; set; } = RpgEventType.StoryProgress;

    /// <summary>
    /// Moment de la session où l'événement s'est produit.
    /// </summary>
    public DateTime? OccurredAt { get; set; }

    /// <summary>
    /// Identifiant du personnage principal impliqué (optionnel).
    /// </summary>
    public int? InvolvedCharacterId { get; set; }

    /// <summary>
    /// Importance de l'événement pour l'histoire.
    /// </summary>
    [Range(1, 5)]
    public int ImportanceLevel { get; set; } = 3;

    /// <summary>
    /// Indique si l'événement est visible par tous les joueurs.
    /// </summary>
    public bool IsPublic { get; set; } = true;

    /// <summary>
    /// Tags associés à l'événement pour faciliter la recherche.
    /// </summary>
    [StringLength(500)]
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// Date de création de l'enregistrement.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Identifiant du créateur (joueur ou MJ).
    /// </summary>
    [Required]
    public int CreatedById { get; set; }
}

/// <summary>
/// Types d'événements possibles dans une session de jeu de rôle.
/// </summary>
public enum RpgEventType
{
    /// <summary>
    /// Progression de l'histoire principale.
    /// </summary>
    StoryProgress,

    /// <summary>
    /// Combat contre des ennemis.
    /// </summary>
    Combat,

    /// <summary>
    /// Interaction sociale ou négociation.
    /// </summary>
    Social,

    /// <summary>
    /// Découverte d'un lieu important.
    /// </summary>
    Discovery,

    /// <summary>
    /// Acquisition d'un objet ou trésor.
    /// </summary>
    Loot,

    /// <summary>
    /// Mort ou blessure grave d'un personnage.
    /// </summary>
    CharacterDeath,

    /// <summary>
    /// Gain de niveau d'un personnage.
    /// </summary>
    LevelUp,

    /// <summary>
    /// Rencontre avec un PNJ important.
    /// </summary>
    NpcEncounter,

    /// <summary>
    /// Résolution d'une quête.
    /// </summary>
    QuestCompletion,

    /// <summary>
    /// Événement hors-jeu (pause, problème technique, etc.).
    /// </summary>
    OutOfGame
}