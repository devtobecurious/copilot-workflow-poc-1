using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente une session de jeu avec ses participants.
/// </summary>
public class GameSession
{
    /// <summary>
    /// Identifiant unique de la session.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de la session de jeu.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant du créateur de la session.
    /// </summary>
    [Required]
    public int CreatorId { get; set; }

    /// <summary>
    /// Date et heure de création de la session.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indique si la session est active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date et heure de fin de la session (optionnelle).
    /// </summary>
    public DateTime? EndedAt { get; set; }

    /// <summary>
    /// Liste des participants de la session avec leur statut.
    /// </summary>
    public List<SessionFriend> Participants { get; set; } = new();

    /// <summary>
    /// Statistiques associées à cette session.
    /// </summary>
    public List<Statistique> Statistics { get; set; } = new();
}