namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un deck Magix associé à un ami.
/// </summary>
/// <param name="Id">Identifiant unique du deck</param>
/// <param name="Name">Nom du deck</param>
/// <param name="FriendId">Identifiant de l'ami propriétaire du deck</param>
public record DeckMagix(int Id, string Name, int FriendId);