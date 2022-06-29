using System;
using System.Collections.Generic;
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

    [Fact]
    public void Ctor_ShouldEmitACtorCreatedEvent_WhenPassedValues()
    {
        _sut.Events.Should()
            .Contain(new ActorCreated(ActorId.Empty, new Name("John", null, "Doe"), new DateOnly(1998, 11, 10)));
    }

    [Fact]
    public void GetMemento_ShouldSetIdInMemento_WhenInvoked()
    {
        var guid = Guid.NewGuid();
        var sut = new Actor(new ActorId(guid), new Name("John", null, "Doe"), new DateOnly(1998, 11, 10));
        
        var memento = sut.GetMemento();

        memento.Id.Should().Be(guid);
    }

    [Fact]
    public void GetMemento_ShouldSetNameInMemento_WhenInvoked()
    {
        var memento = _sut.GetMemento();

        memento.Name.Should().Be(new Name("John", null, "Doe"));
    }

    [Fact]
    public void GetMemento_ShouldSetDateOfBirth_WhenInvoked()
    {
        var memento = _sut.GetMemento();

        memento.DateOfBirth.Should().Be(new DateOnly(1998, 11, 10));
    }

    [Fact]
    public void GetMemento_ShouldSetDateOfDeathToNull_WhenActorNotDead()
    {
        var memento = _sut.GetMemento();

        memento.DateOfDeath.Should().Be(null);
    }

    [Fact]
    public void GetMemento_ShouldSetDateOfDeath_WhenActorIsDead()
    {
        var dateOfDeath = new DateOnly(2022, 10, 05);
        _sut.Died(dateOfDeath);
        
        var memento = _sut.GetMemento();

        memento.DateOfDeath.Should().Be(dateOfDeath);
    }

    [Fact]
    public void GetMemento_ShouldSetAlternateNames_WhenInvoked()
    {
        _sut.ChangeName(new Name("Tony", null, "Robinson"));
        
        var memento = _sut.GetMemento();

        memento.AlternateNames.Should().Equal(new Name("John", null, "Doe"));
    }

    [Fact]
    public void Ctor_ShouldSetFields_WhenPassedMemento()
    {
        var guid = Guid.NewGuid();
        var memento = new ActorMemento()
        {
            Id = guid,
            Name = new Name("Hugh", null, "Laurie"),
            DateOfBirth = new DateOnly(1959, 6, 11),
            DateOfDeath = new DateOnly(2043, 10, 02),
            AlternateNames = new List<Name>(new [] { new Name("James", "Hugh Calum", "Laurie")})
        };

        var sut = new Actor(memento);

        sut.Id.Should().Be(new ActorId(memento.Id));
        sut.Name.Should().Be(memento.Name);
        sut.DateOfBirth.Should().Be(memento.DateOfBirth);
        sut.DateOfDeath.Should().Be(memento.DateOfDeath);
        sut.AlternateNames.Should().Equal(memento.AlternateNames);
    }

    [Fact]
    public void Ctor_ShouldThrowException_WhenPassedMementoWithNullName()
    {
        var guid = Guid.NewGuid();
        var memento = new ActorMemento()
        {
            Id = guid,
            Name = null,
            DateOfBirth = new DateOnly(1959, 6, 11),
            DateOfDeath = new DateOnly(2043, 10, 02),
            AlternateNames = new List<Name>(new [] { new Name("James", "Hugh Calum", "Laurie")})
        };

        var action = () => new Actor(memento);

        action.Should().Throw<ArgumentNullException>().WithParameterName("memento.Name");
    }

}