using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un genre de jeu vidéo.
/// </summary>
public class Genre
{
    /// <summary>
    /// Identifiant unique du genre.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du genre de jeu vidéo.
    /// </summary>
    [Required(ErrorMessage = "Le nom du genre est requis")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom du genre doit contenir entre 2 et 100 caractères")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description du genre.
    /// </summary>
    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string? Description { get; set; }

    /// <summary>
    /// Indique si le genre est actif et visible.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date de création du genre.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Liste des jeux vidéo appartenant à ce genre.
    /// </summary>
    public List<VideoGame> VideoGames { get; set; } = new();
}