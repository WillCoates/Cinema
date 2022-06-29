using System.Threading;
using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Commands;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Tests.Unit.Actors;

public class CreateActorHandlerTests
{
    private readonly CreateActorHandler _sut;
    private readonly Mock<IActorRepository> _actorRepository;
    
    public CreateActorHandlerTests()
    {
        _actorRepository = new Mock<IActorRepository>();
        _sut = new CreateActorHandler(_actorRepository.Object);
    }
    
    [Fact]
    public async Task CreateActor_ShouldCreateNewActor_WithValidCommand()
    {
        var request = new CreateActor(
            "Stephen", "John", "Fry", new DateOnly(1957, 8, 24)
        );
        var actorId = new ActorId(Guid.NewGuid());
        _actorRepository.Setup(x => x.NewId().Result)
            .Returns(actorId);

        await _sut.Handle(request, CancellationToken.None);
        
        _actorRepository.Verify(
            x => x.Create(
                It.Is<Actor>(
                    actor => actor.Id == actorId
                        && actor.Name == new Name("Stephen", "John", "Fry")
                        && actor.DateOfBirth == new DateOnly(1957, 8, 24)
                )
            )
        );
    }
}