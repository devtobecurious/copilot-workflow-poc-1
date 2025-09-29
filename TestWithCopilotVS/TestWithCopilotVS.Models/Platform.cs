using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente une plateforme de jeu (console, PC, mobile, etc.).
/// </summary>
public class Platform
{
    /// <summary>
    /// Identifiant unique de la plateforme.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de la plateforme.
    /// </summary>
    [Required(ErrorMessage = "Le nom de la plateforme est requis")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom de la plateforme doit contenir entre 2 et 100 caractères")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nom court ou abréviation de la plateforme.
    /// </summary>
    [StringLength(20, ErrorMessage = "Le nom court ne peut pas dépasser 20 caractères")]
    public string? ShortName { get; set; }

    /// <summary>
    /// Fabricant de la plateforme.
    /// </summary>
    [StringLength(100, ErrorMessage = "Le nom du fabricant ne peut pas dépasser 100 caractères")]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Type de plateforme.
    /// </summary>
    public PlatformType Type { get; set; } = PlatformType.Unknown;

    /// <summary>
    /// Date de lancement de la plateforme.
    /// </summary>
    public DateTime? ReleaseDate { get; set; }

    /// <summary>
    /// Indique si la plateforme est encore supportée.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date de création de l'enregistrement.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Liste des jeux vidéo disponibles sur cette plateforme.
    /// </summary>
    public List<VideoGame> VideoGames { get; set; } = new();
}

/// <summary>
/// Énumération des types de plateformes.
/// </summary>
public enum PlatformType
{
    Unknown = 0,
    Console = 1,
    PC = 2,
    Mobile = 3,
    Handheld = 4,
    VR = 5,
    Arcade = 6,
    Cloud = 7
}