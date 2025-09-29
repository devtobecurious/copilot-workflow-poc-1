using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;
using Xunit;

namespace TestWithCopilotVS.Tests;

/// <summary>
/// Tests unitaires pour le repository des jeux vidéo.
/// </summary>
public class VideoGameRepositoryTests
{
    private readonly IVideoGameRepository _repository;

    public VideoGameRepositoryTests()
    {
        _repository = new InMemoryVideoGameRepository();
    }

    [Fact]
    public void GetAll_ShouldReturnSampleVideoGames()
    {
        // Act
        var videoGames = _repository.GetAll();

        // Assert
        Assert.NotNull(videoGames);
        Assert.True(videoGames.Count() >= 3);
        Assert.Contains(videoGames, vg => vg.Title.Contains("Zelda"));
        Assert.Contains(videoGames, vg => vg.Title.Contains("Cyberpunk"));
        Assert.Contains(videoGames, vg => vg.Title.Contains("Mario"));
    }

    [Fact]
    public void Get_WithValidId_ShouldReturnVideoGame()
    {
        // Act
        var videoGame = _repository.Get(1);

        // Assert
        Assert.NotNull(videoGame);
        Assert.Equal("The Legend of Zelda: Breath of the Wild", videoGame.Title);
        Assert.Equal("Nintendo", videoGame.Publisher.Name);
    }

    [Fact]
    public void Get_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var videoGame = _repository.Get(999);

        // Assert
        Assert.Null(videoGame);
    }

    [Fact]
    public void Add_WithValidVideoGame_ShouldReturnVideoGameWithId()
    {
        // Arrange
        var publisher = new Publisher { Id = 1, Name = "Test Publisher", IsActive = true };
        var genre = new Genre { Id = 1, Name = "Test Genre", IsActive = true };
        var platform = new Platform { Id = 1, Name = "Test Platform", IsActive = true };

        var videoGame = new VideoGame
        {
            Title = "Test Game",
            Description = "A test game",
            PublisherId = publisher.Id,
            Publisher = publisher,
            ReleaseDate = DateTime.Now,
            Price = 29.99m,
            Genres = [genre],
            Platforms = [platform],
            EsrbRating = EsrbRating.Everyone
        };

        // Act
        var result = _repository.Add(videoGame);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Test Game", result.Title);
        Assert.True(result.CreatedAt <= DateTime.UtcNow);
        Assert.True(result.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void SearchByTitle_WithExistingTitle_ShouldReturnResults()
    {
        // Act
        var results = _repository.SearchByTitle("Zelda");

        // Assert
        Assert.NotNull(results);
        Assert.Single(results);
        Assert.Contains(results, vg => vg.Title.Contains("Zelda"));
    }

    [Fact]
    public void SearchByTitle_WithNonExistingTitle_ShouldReturnEmptyResults()
    {
        // Act
        var results = _repository.SearchByTitle("NonExistentGame");

        // Assert
        Assert.NotNull(results);
        Assert.Empty(results);
    }

    [Fact]
    public void GetAvailable_ShouldReturnOnlyAvailableGames()
    {
        // Act
        var availableGames = _repository.GetAvailable();

        // Assert
        Assert.NotNull(availableGames);
        Assert.All(availableGames, vg => Assert.True(vg.IsAvailable));
    }

    [Fact]
    public void GetByPriceRange_WithValidRange_ShouldReturnGamesInRange()
    {
        // Act
        var gamesInRange = _repository.GetByPriceRange(40m, 60m);

        // Assert
        Assert.NotNull(gamesInRange);
        Assert.All(gamesInRange, vg => 
        {
            Assert.True(vg.Price >= 40m);
            Assert.True(vg.Price <= 60m);
        });
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateVideoGame()
    {
        // Arrange
        var originalGame = _repository.Get(1);
        Assert.NotNull(originalGame);

        var updatedTitle = "Updated Title";
        originalGame.Title = updatedTitle;

        // Act
        var result = _repository.Update(1, originalGame);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedTitle, result.Title);
        Assert.True(result.UpdatedAt > result.CreatedAt);
    }

    [Fact]
    public void Delete_WithValidId_ShouldRemoveVideoGame()
    {
        // Arrange
        var initialCount = _repository.GetAll().Count();

        // Act
        var deleted = _repository.Delete(1);
        var finalCount = _repository.GetAll().Count();

        // Assert
        Assert.True(deleted);
        Assert.Equal(initialCount - 1, finalCount);
        Assert.Null(_repository.Get(1));
    }

    [Fact]
    public void Delete_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var deleted = _repository.Delete(999);

        // Assert
        Assert.False(deleted);
    }
}