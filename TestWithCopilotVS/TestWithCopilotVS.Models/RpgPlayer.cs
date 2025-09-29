using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un joueur dans une campagne de jeu de rôle.
/// </summary>
public class RpgPlayer
{
    /// <summary>
    /// Identifiant unique du joueur.
    /// </summary>
    public int Id { get; set; }

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
    /// Identifiant de la campagne à laquelle le joueur participe.
    /// </summary>
    [Required]
    public int CampaignId { get; set; }

    /// <summary>
    /// Date de rejointe de la campagne.
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indique si le joueur est actif dans la campagne.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Rôle du joueur (Player, GameMaster, Co-GM).
    /// </summary>
    [Required]
    public RpgPlayerRole Role { get; set; } = RpgPlayerRole.Player;

    /// <summary>
    /// Niveau d'expérience du joueur en jeu de rôle (débutant, intermédiaire, avancé).
    /// </summary>
    public RpgExperienceLevel ExperienceLevel { get; set; } = RpgExperienceLevel.Beginner;

    /// <summary>
    /// Personnages créés par ce joueur dans la campagne.
    /// </summary>
    public List<RpgCharacter> Characters { get; set; } = new();
}

/// <summary>
/// Énumération des rôles possibles pour un joueur.
/// </summary>
public enum RpgPlayerRole
{
    /// <summary>
    /// Joueur standard.
    /// </summary>
    Player,

    /// <summary>
    /// Maître de jeu principal.
    /// </summary>
    GameMaster,

    /// <summary>
    /// Co-maître de jeu ou assistant.
    /// </summary>
    CoGameMaster
}

/// <summary>
/// Énumération des niveaux d'expérience en jeu de rôle.
/// </summary>
public enum RpgExperienceLevel
{
    /// <summary>
    /// Débutant, peu ou pas d'expérience.
    /// </summary>
    Beginner,

    /// <summary>
    /// Intermédiaire, quelques campagnes d'expérience.
    /// </summary>
    Intermediate,

    /// <summary>
    /// Avancé, joueur expérimenté.
    /// </summary>
    Advanced,

    /// <summary>
    /// Expert, joueur très expérimenté ou professionnel.
    /// </summary>
    Expert
}