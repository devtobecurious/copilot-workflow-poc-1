namespace TestWithCopilotVS;

public interface IFriendRepository
{
    IEnumerable<Friend> GetAll();
    Friend? Get(int id);
    Friend Add(Friend friend);
    Friend? Update(int id, Friend friend);
    bool Delete(int id);
}

public class InMemoryFriendRepository : IFriendRepository
{
    private readonly List<Friend> _friends = new()
    {
        new Friend(1, "Alice", "alice@example.com"),
        new Friend(2, "Bob", "bob@example.com"),
        new Friend(3, "Charlie", null)
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
}
