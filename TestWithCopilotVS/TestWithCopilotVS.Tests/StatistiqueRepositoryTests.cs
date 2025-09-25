using System.Collections.Generic;
using Xunit;
using TestWithCopilotVS;

namespace TestWithCopilotVS.Tests
{
    public class StatistiqueRepositoryTests
    {
        [Fact]
        public void Add_ShouldAssignIdAndStoreStatistique()
        {
            var repo = new StatistiqueRepository();
            var stat = new Statistique { NbSuccess = 2, AmisPresents = new List<string> { "Alice", "Bob" }, Gagnant = "Alice", Mois = 9, Annee = 2025 };

            var result = repo.Add(stat);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectStatistique()
        {
            var repo = new StatistiqueRepository();
            var stat = repo.Add(new Statistique { NbSuccess = 1, AmisPresents = new List<string> { "Bob" }, Gagnant = "Bob", Mois = 8, Annee = 2025 });

            var found = repo.GetById(stat.Id);

            Assert.NotNull(found);
            Assert.Equal("Bob", found.Gagnant);
        }

        [Fact]
        public void Delete_ShouldRemoveStatistique()
        {
            var repo = new StatistiqueRepository();
            var stat = repo.Add(new Statistique { NbSuccess = 3, AmisPresents = new List<string> { "Eve" }, Gagnant = "Eve", Mois = 7, Annee = 2025 });

            var deleted = repo.Delete(stat.Id);

            Assert.True(deleted);
            var found = repo.GetById(stat.Id);
            Assert.Null(found);
        }
    }
}
