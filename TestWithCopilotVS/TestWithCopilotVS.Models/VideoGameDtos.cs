using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// DTO pour la création d'un nouveau jeu vidéo.
/// </summary>
public class CreateVideoGameDto
{
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
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de l'éditeur doit être positif")]
    public int PublisherId { get; set; }

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
    /// Liste des identifiants des genres associés au jeu.
    /// </summary>
    public List<int> GenreIds { get; set; } = new();

    /// <summary>
    /// Liste des identifiants des plateformes sur lesquelles le jeu est disponible.
    /// </summary>
    public List<int> PlatformIds { get; set; } = new();

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

/// <summary>
/// DTO pour la mise à jour d'un jeu vidéo.
/// </summary>
public class UpdateVideoGameDto
{
    /// <summary>
    /// Titre du jeu vidéo.
    /// </summary>
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Le titre doit contenir entre 1 et 200 caractères")]
    public string? Title { get; set; }

    /// <summary>
    /// Description du jeu vidéo.
    /// </summary>
    [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
    public string? Description { get; set; }

    /// <summary>
    /// Identifiant de l'éditeur du jeu.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de l'éditeur doit être positif")]
    public int? PublisherId { get; set; }

    /// <summary>
    /// Date de sortie du jeu.
    /// </summary>
    public DateTime? ReleaseDate { get; set; }

    /// <summary>
    /// Note métacritic du jeu (0-100).
    /// </summary>
    [Range(0, 100, ErrorMessage = "La note Metacritic doit être entre 0 et 100")]
    public int? MetacriticScore { get; set; }

    /// <summary>
    /// Prix du jeu en euros.
    /// </summary>
    [Range(0.0, 999.99, ErrorMessage = "Le prix doit être entre 0 et 999.99 euros")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Indique si le jeu est disponible à la vente.
    /// </summary>
    public bool? IsAvailable { get; set; }

    /// <summary>
    /// Liste des identifiants des genres associés au jeu.
    /// </summary>
    public List<int>? GenreIds { get; set; }

    /// <summary>
    /// Liste des identifiants des plateformes sur lesquelles le jeu est disponible.
    /// </summary>
    public List<int>? PlatformIds { get; set; }

    /// <summary>
    /// URL de l'image de couverture du jeu.
    /// </summary>
    [Url(ErrorMessage = "L'URL de l'image de couverture n'est pas valide")]
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Classement ESRB du jeu.
    /// </summary>
    public EsrbRating? EsrbRating { get; set; }
}

/// <summary>
/// DTO de réponse pour les informations d'un jeu vidéo.
/// </summary>
public class VideoGameResponseDto
{
    /// <summary>
    /// Identifiant unique du jeu vidéo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre du jeu vidéo.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description du jeu vidéo.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Informations sur l'éditeur du jeu.
    /// </summary>
    public PublisherResponseDto Publisher { get; set; } = null!;

    /// <summary>
    /// Date de sortie du jeu.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Note métacritic du jeu.
    /// </summary>
    public int? MetacriticScore { get; set; }

    /// <summary>
    /// Prix du jeu en euros.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Indique si le jeu est disponible à la vente.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Date d'ajout du jeu dans le système.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date de dernière mise à jour.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Liste des genres du jeu.
    /// </summary>
    public List<GenreResponseDto> Genres { get; set; } = new();

    /// <summary>
    /// Liste des plateformes du jeu.
    /// </summary>
    public List<PlatformResponseDto> Platforms { get; set; } = new();

    /// <summary>
    /// URL de l'image de couverture du jeu.
    /// </summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Classement ESRB du jeu.
    /// </summary>
    public EsrbRating EsrbRating { get; set; }
}

/// <summary>
/// DTO de réponse simplifié pour les listes de jeux.
/// </summary>
public class VideoGameSummaryDto
{
    /// <summary>
    /// Identifiant unique du jeu vidéo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titre du jeu vidéo.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Nom de l'éditeur.
    /// </summary>
    public string PublisherName { get; set; } = string.Empty;

    /// <summary>
    /// Date de sortie du jeu.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Prix du jeu en euros.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Note métacritic du jeu.
    /// </summary>
    public int? MetacriticScore { get; set; }

    /// <summary>
    /// Indique si le jeu est disponible à la vente.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// URL de l'image de couverture du jeu.
    /// </summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Classement ESRB du jeu.
    /// </summary>
    public EsrbRating EsrbRating { get; set; }
}

/// <summary>
/// DTO de réponse pour les informations d'un éditeur.
/// </summary>
public class PublisherResponseDto
{
    /// <summary>
    /// Identifiant unique de l'éditeur.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de l'éditeur.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Site web de l'éditeur.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Pays d'origine.
    /// </summary>
    public string? Country { get; set; }
}

/// <summary>
/// DTO de réponse pour les informations d'un genre.
/// </summary>
public class GenreResponseDto
{
    /// <summary>
    /// Identifiant unique du genre.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du genre.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description du genre.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// DTO de réponse pour les informations d'une plateforme.
/// </summary>
public class PlatformResponseDto
{
    /// <summary>
    /// Identifiant unique de la plateforme.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de la plateforme.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nom court de la plateforme.
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Type de plateforme.
    /// </summary>
    public PlatformType Type { get; set; }
}