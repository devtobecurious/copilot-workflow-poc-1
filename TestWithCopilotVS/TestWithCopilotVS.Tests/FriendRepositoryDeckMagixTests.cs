using System.Collections.Generic;
using System.Linq;
using TestWithCopilotVS;
using Xunit;

namespace TestWithCopilotVS.Tests
{
    public class FriendRepositoryDeckMagixTests
    {
        [Fact]
        public void GetAllDecksMagix_ReturnsAllDecks()
        {
            // Arrange
            var repo = new InMemoryFriendRepository();

            // Act
            var decks = repo.GetAllDecksMagix().ToList();

            // Assert
            Assert.NotNull(decks);
            Assert.Equal(4, decks.Count);
            Assert.Contains(decks, d => d.Name == "Deck Elfe" && d.FriendId == 1);
            Assert.Contains(decks, d => d.Name == "Deck Gobelin" && d.FriendId == 1);
            Assert.Contains(decks, d => d.Name == "Deck Vampire" && d.FriendId == 2);
            Assert.Contains(decks, d => d.Name == "Deck Dragon" && d.FriendId == 3);
        }
    }
}
