using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Domain.Tests.Unit.Actors;

public class NameTests
{
    [Fact]
    public void ToString_ShouldConcatenateNames_WhenForenameAndSurnameSet()
    {
        var name = new Name("John", null, "Doe");

        var displayName = name.ToString();

        displayName.Should().Be("John Doe");
    }
    
    [Fact]
    public void ToString_ShouldIncludeMiddleNames_WhenMiddleNamesSet()
    {
        var name = new Name("John", "Alexander", "Doe");

        var displayName = name.ToString();

        displayName.Should().Be("John Alexander Doe");
    }
}