using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// DTO pour la création d'une nouvelle campagne de jeu de rôle.
/// </summary>
public class CreateRpgCampaignDto
{
    /// <summary>
    /// Titre de la campagne.
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description de la campagne.
    /// </summary>
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Système de jeu utilisé.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string GameSystem { get; set; } = string.Empty;

    /// <summary>
    /// Date de début prévue.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Niveau minimum requis.
    /// </summary>
    [Range(1, 20)]
    public int MinimumLevel { get; set; } = 1;

    /// <summary>
    /// Niveau maximum autorisé.
    /// </summary>
    [Range(1, 20)]
    public int MaximumLevel { get; set; } = 20;

    /// <summary>
    /// Nombre maximum de joueurs.
    /// </summary>
    [Range(1, 8)]
    public int MaxPlayers { get; set; } = 6;
}

/// <summary>
/// DTO pour la mise à jour d'une campagne existante.
/// </summary>
public class UpdateRpgCampaignDto
{
    /// <summary>
    /// Titre de la campagne.
    /// </summary>
    [StringLength(200, MinimumLength = 3)]
    public string? Title { get; set; }

    /// <summary>
    /// Description de la campagne.
    /// </summary>
    [StringLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// Date de début prévue.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Date de fin prévue.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Statut actif de la campagne.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Nombre maximum de joueurs.
    /// </summary>
    [Range(1, 8)]
    public int? MaxPlayers { get; set; }
}

/// <summary>
/// DTO pour la création d'un nouveau personnage.
/// </summary>
public class CreateRpgCharacterDto
{
    /// <summary>
    /// Nom du personnage.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Classe du personnage.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string CharacterClass { get; set; } = string.Empty;

    /// <summary>
    /// Race du personnage.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Race { get; set; } = string.Empty;

    /// <summary>
    /// Niveau du personnage.
    /// </summary>
    [Range(1, 20)]
    public int Level { get; set; } = 1;

    /// <summary>
    /// Points de vie maximum.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MaxHitPoints { get; set; } = 1;

    /// <summary>
    /// Histoire du personnage.
    /// </summary>
    [StringLength(2000)]
    public string Background { get; set; } = string.Empty;

    /// <summary>
    /// Indique si c'est le personnage principal.
    /// </summary>
    public bool IsMainCharacter { get; set; } = false;
}

/// <summary>
/// DTO pour la planification d'une nouvelle session.
/// </summary>
public class CreateRpgSessionDto
{
    /// <summary>
    /// Titre de la session.
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Date et heure de début.
    /// </summary>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Durée prévue en minutes.
    /// </summary>
    [Range(30, 600)]
    public int PlannedDurationMinutes { get; set; } = 240;

    /// <summary>
    /// Notes du maître de jeu.
    /// </summary>
    [StringLength(10000)]
    public string GameMasterNotes { get; set; } = string.Empty;
}

/// <summary>
/// DTO pour rejoindre une campagne en tant que joueur.
/// </summary>
public class JoinRpgCampaignDto
{
    /// <summary>
    /// Nom d'utilisateur du joueur.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email du joueur.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Niveau d'expérience du joueur.
    /// </summary>
    public RpgExperienceLevel ExperienceLevel { get; set; } = RpgExperienceLevel.Beginner;
}

/// <summary>
/// DTO pour mettre à jour la participation à une session.
/// </summary>
public class UpdateRpgSessionParticipationDto
{
    /// <summary>
    /// Nouveau statut de participation.
    /// </summary>
    public RpgParticipationStatus? Status { get; set; }

    /// <summary>
    /// Identifiant du personnage à utiliser.
    /// </summary>
    public int? CharacterId { get; set; }

    /// <summary>
    /// Notes du joueur.
    /// </summary>
    [StringLength(2000)]
    public string? PlayerNotes { get; set; }
}