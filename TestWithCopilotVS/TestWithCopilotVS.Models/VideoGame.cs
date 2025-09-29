using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un jeu vidéo dans le système.
/// </summary>
public class VideoGame
{
    /// <summary>
    /// Identifiant unique du jeu vidéo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre du jeu vidéo.
    /// </summary>
    [Required(ErrorMessage = "Le titre du jeu est requis")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Le titre doit contenir entre 1 et 200 caractères")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description du jeu vidéo.
    /// </summary>
    [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
    public string? Description { get; set; }

    /// <summary>
    /// Identifiant de l'éditeur du jeu.
    /// </summary>
    [Required(ErrorMessage = "L'éditeur est requis")]
    public int PublisherId { get; set; }

    /// <summary>
    /// Éditeur du jeu.
    /// </summary>
    public Publisher Publisher { get; set; } = null!;

    /// <summary>
    /// Date de sortie du jeu.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Note métacritic du jeu (0-100).
    /// </summary>
    [Range(0, 100, ErrorMessage = "La note Metacritic doit être entre 0 et 100")]
    public int? MetacriticScore { get; set; }

    /// <summary>
    /// Prix du jeu en euros.
    /// </summary>
    [Range(0.0, 999.99, ErrorMessage = "Le prix doit être entre 0 et 999.99 euros")]
    public decimal Price { get; set; }

    /// <summary>
    /// Indique si le jeu est actuellement disponible à la vente.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Date d'ajout du jeu dans le système.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de dernière mise à jour des informations du jeu.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Liste des genres associés au jeu.
    /// </summary>
    public List<Genre> Genres { get; set; } = new();

    /// <summary>
    /// Liste des plateformes sur lesquelles le jeu est disponible.
    /// </summary>
    public List<Platform> Platforms { get; set; } = new();

    /// <summary>
    /// URL de l'image de couverture du jeu.
    /// </summary>
    [Url(ErrorMessage = "L'URL de l'image de couverture n'est pas valide")]
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Classement ESRB du jeu.
    /// </summary>
    public EsrbRating EsrbRating { get; set; } = EsrbRating.NotRated;
}