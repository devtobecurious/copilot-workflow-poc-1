using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente les statistiques d'un personnage dans le contexte des sessions de jeu de rôle.
/// </summary>
public class RpgCharacterStatistic
{
    /// <summary>
    /// Identifiant unique de la statistique.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant du personnage.
    /// </summary>
    [Required]
    public int CharacterId { get; set; }

    /// <summary>
    /// Identifiant de la session (optionnel pour les statistiques globales).
    /// </summary>
    public int? SessionId { get; set; }

    /// <summary>
    /// Type de statistique.
    /// </summary>
    [Required]
    public RpgStatisticType StatisticType { get; set; }

    /// <summary>
    /// Nom de la statistique.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Valeur de la statistique.
    /// </summary>
    [Required]
    public int Value { get; set; } = 0;

    /// <summary>
    /// Valeur précédente (pour tracking des changements).
    /// </summary>
    public int? PreviousValue { get; set; }

    /// <summary>
    /// Description ou contexte de la statistique.
    /// </summary>
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date de création ou mise à jour.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indique si cette statistique est visible par les autres joueurs.
    /// </summary>
    public bool IsPublic { get; set; } = true;

    /// <summary>
    /// Identifiant du créateur/modificateur (joueur ou MJ).
    /// </summary>
    [Required]
    public int ModifiedById { get; set; }
}

/// <summary>
/// Types de statistiques possibles pour un personnage.
/// </summary>
public enum RpgStatisticType
{
    /// <summary>
    /// Points d'expérience gagnés.
    /// </summary>
    ExperienceGained,

    /// <summary>
    /// Nombre de victoires en combat.
    /// </summary>
    CombatVictories,

    /// <summary>
    /// Nombre de défaites en combat.
    /// </summary>
    CombatDefeats,

    /// <summary>
    /// Dégâts infligés.
    /// </summary>
    DamageDealt,

    /// <summary>
    /// Dégâts subis.
    /// </summary>
    DamageTaken,

    /// <summary>
    /// Nombre de sorts lancés.
    /// </summary>
    SpellsCast,

    /// <summary>
    /// Nombre de critiques réussis.
    /// </summary>
    CriticalHits,

    /// <summary>
    /// Nombre d'échecs critiques.
    /// </summary>
    CriticalFailures,

    /// <summary>
    /// Points de vie soignés (sur soi ou sur d'autres).
    /// </summary>
    HitPointsHealed,

    /// <summary>
    /// Nombre d'objets trouvés.
    /// </summary>
    ItemsFound,

    /// <summary>
    /// Or ou richesses accumulées.
    /// </summary>
    WealthAccumulated,

    /// <summary>
    /// Nombre de quêtes accomplies.
    /// </summary>
    QuestsCompleted,

    /// <summary>
    /// Nombre d'interactions sociales réussies.
    /// </summary>
    SocialSuccesses,

    /// <summary>
    /// Nombre de fois où le personnage est tombé inconscient.
    /// </summary>
    TimesUnconscious,

    /// <summary>
    /// Nombre de morts/résurrections du personnage.
    /// </summary>
    Deaths,

    /// <summary>
    /// Statistique personnalisée définie par le MJ.
    /// </summary>
    Custom
}