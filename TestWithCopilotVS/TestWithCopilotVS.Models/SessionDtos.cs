using System.ComponentModel.DataAnnotations;

namespace TestWithCopilotVS.Models;

/// <summary>
/// DTO pour la création d'une nouvelle session de jeu.
/// </summary>
public class CreateGameSessionDto
{
    /// <summary>
    /// Nom de la session de jeu.
    /// </summary>
    [Required(ErrorMessage = "Le nom de la session est requis")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre 3 et 100 caractères")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant du créateur de la session.
    /// </summary>
    [Required(ErrorMessage = "L'identifiant du créateur est requis")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant du créateur doit être positif")]
    public int CreatorId { get; set; }

    /// <summary>
    /// Liste des identifiants des amis primaires à inviter.
    /// </summary>
    public List<int> InitialFriendIds { get; set; } = new();
}

/// <summary>
/// DTO pour ajouter un ami à une session existante.
/// </summary>
public class AddFriendToSessionDto
{
    /// <summary>
    /// Identifiant de l'ami à ajouter.
    /// </summary>
    [Required(ErrorMessage = "L'identifiant de l'ami est requis")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de l'ami doit être positif")]
    public int FriendId { get; set; }

    /// <summary>
    /// Statut de l'ami dans la session.
    /// </summary>
    public FriendSessionStatus Status { get; set; } = FriendSessionStatus.Secondary;

    /// <summary>
    /// Message personnalisé pour l'invitation (optionnel).
    /// </summary>
    [StringLength(500, ErrorMessage = "Le message ne peut pas dépasser 500 caractères")]
    public string? Message { get; set; }
}

/// <summary>
/// DTO pour modifier le statut d'un ami dans une session.
/// </summary>
public class UpdateFriendStatusDto
{
    /// <summary>
    /// Nouveau statut de l'ami dans la session.
    /// </summary>
    [Required(ErrorMessage = "Le statut est requis")]
    public FriendSessionStatus Status { get; set; }
}

/// <summary>
/// DTO de réponse pour les informations d'une session.
/// </summary>
public class GameSessionResponseDto
{
    /// <summary>
    /// Identifiant unique de la session.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom de la session.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Informations sur le créateur.
    /// </summary>
    public Friend Creator { get; set; } = null!;

    /// <summary>
    /// Date de création de la session.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indique si la session est active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Date de fin de la session.
    /// </summary>
    public DateTime? EndedAt { get; set; }

    /// <summary>
    /// Liste des participants avec leurs informations.
    /// </summary>
    public List<SessionFriendResponseDto> Participants { get; set; } = new();

    /// <summary>
    /// Nombre total de participants.
    /// </summary>
    public int ParticipantCount => Participants.Count;
}

/// <summary>
/// DTO de réponse pour les informations d'un participant de session.
/// </summary>
public class SessionFriendResponseDto
{
    /// <summary>
    /// Informations de l'ami.
    /// </summary>
    public Friend Friend { get; set; } = null!;

    /// <summary>
    /// Statut dans la session.
    /// </summary>
    public FriendSessionStatus Status { get; set; }

    /// <summary>
    /// Date d'ajout à la session.
    /// </summary>
    public DateTime JoinedAt { get; set; }

    /// <summary>
    /// Indique si l'ami est actif.
    /// </summary>
    public bool IsActive { get; set; }
}