using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente une campagne de jeu de rôle avec ses paramètres et participants.
/// </summary>
public class RpgCampaign
{
    /// <summary>
    /// Identifiant unique de la campagne.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre de la campagne de jeu de rôle.
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description détaillée de la campagne.
    /// </summary>
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Système de jeu utilisé (D&D 5e, Pathfinder, etc.).
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string GameSystem { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant du maître de jeu.
    /// </summary>
    [Required]
    public int GameMasterId { get; set; }

    /// <summary>
    /// Date de création de la campagne.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de début prévue de la campagne.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Date de fin de la campagne (optionnelle).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Indique si la campagne est active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Niveau minimum requis pour rejoindre la campagne.
    /// </summary>
    [Range(1, 20)]
    public int MinimumLevel { get; set; } = 1;

    /// <summary>
    /// Niveau maximum autorisé dans la campagne.
    /// </summary>
    [Range(1, 20)]
    public int MaximumLevel { get; set; } = 20;

    /// <summary>
    /// Nombre maximum de joueurs autorisés.
    /// </summary>
    [Range(1, 8)]
    public int MaxPlayers { get; set; } = 6;

    /// <summary>
    /// Liste des joueurs participant à la campagne.
    /// </summary>
    public List<RpgPlayer> Players { get; set; } = new();

    /// <summary>
    /// Liste des sessions de jeu de la campagne.
    /// </summary>
    public List<RpgSession> Sessions { get; set; } = new();
}