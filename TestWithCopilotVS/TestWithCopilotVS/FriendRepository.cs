namespace TestWithCopilotVS;


public record DeckMagix(int Id, string Name, int FriendId);

public interface IFriendRepository
{
    IEnumerable<Friend> GetAll();
    Friend? Get(int id);
    Friend Add(Friend friend);
    Friend? Update(int id, Friend friend);
    bool Delete(int id);
    /// <summary>
    /// Retourne la liste de tous les decks Magix de tous les amis.
    /// </summary>
    IEnumerable<DeckMagix> GetAllDecksMagix();
}

public class InMemoryFriendRepository : IFriendRepository
{
    private readonly List<Friend> _friends = new()
    {
        new Friend(1, "Alice", "alice@example.com"),
        new Friend(2, "Bob", "bob@example.com"),
        new Friend(3, "Charlie", null)
    };

    private readonly List<DeckMagix> _decksMagix = new()
    {
        new DeckMagix(1, "Deck Elfe", 1),
        new DeckMagix(2, "Deck Gobelin", 1),
        new DeckMagix(3, "Deck Vampire", 2),
        new DeckMagix(4, "Deck Dragon", 3)
    };

    private int _nextId = 4;

    public IEnumerable<Friend> GetAll() => _friends;
    public Friend? Get(int id) => _friends.FirstOrDefault(f => f.Id == id);
    public Friend Add(Friend friend)
    {
        var created = friend with { Id = _nextId++ };
        _friends.Add(created);
        return created;
    }
    public Friend? Update(int id, Friend friend)
    {
        var index = _friends.FindIndex(f => f.Id == id);
        if (index == -1) return null;
        var updated = friend with { Id = id };
        _friends[index] = updated;
        return updated;
    }
    public bool Delete(int id) => _friends.RemoveAll(f => f.Id == id) > 0;

    /// <summary>
    /// Retourne la liste de tous les decks Magix de tous les amis.
    /// </summary>
    public IEnumerable<DeckMagix> GetAllDecksMagix() => _decksMagix;
}
