namespace TestWithCopilotVS.Models;

/// <summary>
/// Représente un ami dans le système de jeu.
/// </summary>
/// <param name="Id">Identifiant unique de l'ami</param>
/// <param name="Name">Nom de l'ami</param>
/// <param name="Email">Adresse email de l'ami (optionnelle)</param>
public record Friend(int Id, string Name, string? Email);