using System;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Domain.Tests.Unit.Actors;

public class ActorTests
{
    private Actor _sut = new(ActorId.Empty, new Name("John", null, "Doe"), new DateOnly(1998, 11, 10));
    
    [Fact]
    public void ChangeName_ShouldChangeName_WhenNameIsProvided()
    {
        var newName = new Name("Rowan", "Sebastian", "Atkinson");
        
        _sut.ChangeName(newName);

        _sut.Name.Should().Be(newName);
    }

    [Fact]
    public void ChangeName_ShouldAddCurrentNameToAlternateNames_WhenNameIsProvided()
    {
        var newName = new Name("Matilda", null, "Ziegler");
        
        _sut.ChangeName(newName);

        _sut.AlternateNames.Should().Contain(new Name("John", null, "Doe"));
    }

    [Fact]
    public void ChangeName_ShouldNotAddCurrentNameToAlternateNames_WhenNameIsAlreadyAlternate()
    {
        var firstChange = new Name("Paul", null, "Brown");
        var secondChange = new Name("John", null, "Doe");
        var thirdChange = new Name("Rudolph", null, "Walker");
        
        _sut.ChangeName(firstChange);
        _sut.ChangeName(secondChange);
        _sut.ChangeName(thirdChange);

        _sut.AlternateNames.Should().ContainSingle(x => x == secondChange);
    }

    [Fact]
    public void ChangeName_ShouldEmitActorNameChangedEvent_WhenNameIsProvided()
    {
        var newName = new Name("Roger", null, "Sloman");
        
        _sut.ChangeName(newName);

        _sut.Events.Should().Contain(new ActorNameChanged(ActorId.Empty, newName));
    }

    [Fact]
    public void Died_ShouldSetDateOfDeath_WhenDateIsProvided()
    {
        var dateOfDeath = new DateOnly(2022, 05, 01);

        _sut.Died(dateOfDeath);

        _sut.DateOfDeath.Should().Be(dateOfDeath);
    }

    [Fact]
    public void Died_ShouldThrowException_WhenActorAlreadyDead()
    {
        var dateOfDeath = new DateOnly(2022, 05, 01);
        _sut.Died(dateOfDeath);
        
        var action = () => _sut.Died(dateOfDeath);

        action.Should().Throw<ActorAlreadyDeadException>().WithMessage("*Actor already died*");
    }

    [Fact]
    public void Died_ShouldEmitActorDiedEvent_WhenDateIsProvided()
    {
        var dateOfDeath = new DateOnly(2022, 05, 01);
        
        _sut.Died(dateOfDeath);

        _sut.Events.Should().Contain(new ActorDied(ActorId.Empty, dateOfDeath));
    }
}