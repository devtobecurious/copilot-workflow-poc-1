using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un personnage de jeu de rôle créé par un joueur.
/// </summary>
public class RpgCharacter
{
    /// <summary>
    /// Identifiant unique du personnage.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du personnage.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Classe du personnage (Guerrier, Magicien, Voleur, etc.).
    /// </summary>
    [Required]
    [StringLength(50)]
    public string CharacterClass { get; set; } = string.Empty;

    /// <summary>
    /// Race du personnage (Humain, Elfe, Nain, etc.).
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Race { get; set; } = string.Empty;

    /// <summary>
    /// Niveau actuel du personnage.
    /// </summary>
    [Range(1, 20)]
    public int Level { get; set; } = 1;

    /// <summary>
    /// Points d'expérience accumulés.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int ExperiencePoints { get; set; } = 0;

    /// <summary>
    /// Points de vie actuels.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int CurrentHitPoints { get; set; } = 1;

    /// <summary>
    /// Points de vie maximum.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MaxHitPoints { get; set; } = 1;

    /// <summary>
    /// Identifiant du joueur propriétaire du personnage.
    /// </summary>
    [Required]
    public int PlayerId { get; set; }

    /// <summary>
    /// Identifiant de la campagne dans laquelle le personnage évolue.
    /// </summary>
    [Required]
    public int CampaignId { get; set; }

    /// <summary>
    /// Date de création du personnage.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indique si le personnage est actif (vivant et jouable).
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indique si le personnage est actuellement le personnage principal du joueur.
    /// </summary>
    public bool IsMainCharacter { get; set; } = false;

    /// <summary>
    /// Background ou histoire du personnage (optionnel).
    /// </summary>
    [StringLength(2000)]
    public string Background { get; set; } = string.Empty;

    /// <summary>
    /// Notes et informations additionnelles sur le personnage.
    /// </summary>
    [StringLength(5000)]
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Statistiques du personnage dans les sessions.
    /// </summary>
    public List<RpgCharacterStatistic> Statistics { get; set; } = new();
}