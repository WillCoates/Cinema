using System.Threading;
using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Commands;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Tests.Unit.Actors;

public class RegisterActorDeathHandlerTests
{
    private readonly RegisterActorDeathHandler _sut;
    private readonly Mock<IActorRepository> _actorRepository;

    public RegisterActorDeathHandlerTests()
    {
        _actorRepository = new Mock<IActorRepository>();
        _sut = new RegisterActorDeathHandler(_actorRepository.Object);
    }
    
    [Fact]
    public async Task RegisterActorDeath_ShouldSetDateOfDeath_WhenActorExists()
    {
        var actorId = new ActorId(Guid.NewGuid());

        var actor = new Actor(new ActorMemento()
        {
            Id = actorId.Id,
            Name = new Name("Bill", null, "Maynard"),
            DateOfBirth = new DateOnly(1928, 10, 8)
        });

        var request = new RegisterActorDeath(actorId, new DateOnly(2018, 03, 30));
        
        _actorRepository.Setup(x => x.Read(actorId).Result)
            .Returns(actor);

        await _sut.Handle(request, CancellationToken.None);
        
        _actorRepository.Verify(x => x.Update(actor));

        actor.DateOfDeath.Should().Be(new DateOnly(2018, 03, 30));
    }

    [Fact]
    public async Task RegisterActorDeath_ShouldThrowException_WhenActorDoesntExist()
    {
        var actorId = new ActorId(Guid.NewGuid());
        
        var request = new RegisterActorDeath(actorId, new DateOnly(2018, 03, 30));

        _actorRepository.Setup(x => x.Read(actorId).Result)
            .Returns(() => null);

        var action = async () => await _sut.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<ActorDoesNotExistException>()
            .WithMessage("*Can't find Actor*");
    }
}