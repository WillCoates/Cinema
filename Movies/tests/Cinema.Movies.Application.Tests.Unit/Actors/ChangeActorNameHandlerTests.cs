using System.Threading;
using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Commands;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Tests.Unit.Actors;

public class ChangeActorNameHandlerTests
{
    private readonly ChangeActorNameHandler _sut;
    private readonly Mock<IActorRepository> _actorRepository;

    public ChangeActorNameHandlerTests()
    {
        _actorRepository = new Mock<IActorRepository>();
        _sut = new ChangeActorNameHandler(_actorRepository.Object);
    }
    
    [Fact]
    public async Task ChangeActorName_ShouldUpdateName_WhenActorExists()
    {
        var actorId = new ActorId(Guid.NewGuid());

        var actor = new Actor(new ActorMemento()
        {
            Id = actorId.Id,
            Name = new Name("Bill", null, "Maynard"),
            DateOfBirth = new DateTime(1928, 10, 8)
        });

        var request = new ChangeActorName(actorId, "Walter", "Frederick George", "Williams");
        
        _actorRepository.Setup(x => x.Read(actorId).Result)
            .Returns(actor);

        await _sut.Handle(request, CancellationToken.None);
        
        _actorRepository.Verify(x => x.Update(actor));

        actor.Name.Should().Be(new Name("Walter", "Frederick George", "Williams"));
    }

    [Fact]
    public async Task ChangeActorName_ShouldThrowException_WhenActorDoesntExist()
    {
        var actorId = new ActorId(Guid.NewGuid());
        
        var request = new ChangeActorName(actorId, "Walter", "Frederick George", "Williams");

        _actorRepository.Setup(x => x.Read(actorId).Result)
            .Returns(() => null);

        var action = async () => await _sut.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<ActorDoesNotExistException>()
            .WithMessage("*Can't find Actor*");
    }
}