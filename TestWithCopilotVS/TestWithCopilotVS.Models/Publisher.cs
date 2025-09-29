using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un éditeur de jeux vidéo.
/// </summary>
public class Publisher
{
    /// <summary>
    /// Identifiant unique de l'éditeur.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de l'éditeur.
    /// </summary>
    [Required(ErrorMessage = "Le nom de l'éditeur est requis")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Le nom de l'éditeur doit contenir entre 2 et 200 caractères")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Site web officiel de l'éditeur.
    /// </summary>
    [Url(ErrorMessage = "L'URL du site web n'est pas valide")]
    public string? Website { get; set; }

    /// <summary>
    /// Pays d'origine de l'éditeur.
    /// </summary>
    [StringLength(100, ErrorMessage = "Le nom du pays ne peut pas dépasser 100 caractères")]
    public string? Country { get; set; }

    /// <summary>
    /// Année de fondation de l'éditeur.
    /// </summary>
    [Range(1950, 2100, ErrorMessage = "L'année de fondation doit être entre 1950 et 2100")]
    public int? FoundedYear { get; set; }

    /// <summary>
    /// Description de l'éditeur.
    /// </summary>
    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
    public string? Description { get; set; }

    /// <summary>
    /// URL du logo de l'éditeur.
    /// </summary>
    [Url(ErrorMessage = "L'URL du logo n'est pas valide")]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// Indique si l'éditeur est encore actif.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date de création de l'enregistrement.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de dernière mise à jour.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Liste des jeux vidéo publiés par cet éditeur.
    /// </summary>
    public List<VideoGame> VideoGames { get; set; } = new();
}