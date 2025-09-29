using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Repositories.Interfaces;

/// <summary>
/// Interface pour la gestion des amis.
/// </summary>
public interface IFriendRepository
{
    /// <summary>
    /// Récupère tous les amis.
    /// </summary>
    IEnumerable<Friend> GetAll();
    
    /// <summary>
    /// Récupère un ami par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de l'ami</param>
    /// <returns>L'ami trouvé ou null</returns>
    Friend? Get(int id);
    
    /// <summary>
    /// Ajoute un nouvel ami.
    /// </summary>
    /// <param name="friend">L'ami à ajouter</param>
    /// <returns>L'ami ajouté avec son ID généré</returns>
    Friend Add(Friend friend);
    
    /// <summary>
    /// Met à jour un ami existant.
    /// </summary>
    /// <param name="id">Identifiant de l'ami à mettre à jour</param>
    /// <param name="friend">Les nouvelles données de l'ami</param>
    /// <returns>L'ami mis à jour ou null si non trouvé</returns>
    Friend? Update(int id, Friend friend);
    
    /// <summary>
    /// Supprime un ami.
    /// </summary>
    /// <param name="id">Identifiant de l'ami à supprimer</param>
    /// <returns>True si supprimé, false sinon</returns>
    bool Delete(int id);
    
    /// <summary>
    /// Retourne la liste de tous les decks Magix de tous les amis.
    /// </summary>
    IEnumerable<DeckMagix> GetAllDecksMagix();
}