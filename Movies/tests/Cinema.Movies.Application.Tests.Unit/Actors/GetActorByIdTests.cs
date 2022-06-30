using System.Threading;
using AutoMapper;
using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Application.Actors.Queries;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Tests.Unit.Actors;

public class GetActorByIdTests
{
    private readonly GetActorByIdHandler _sut;
    private readonly Mock<IActorReadRepository> _actorReadRepository;

    public GetActorByIdTests()
    {
        _actorReadRepository = new Mock<IActorReadRepository>();
        _sut = new GetActorByIdHandler(_actorReadRepository.Object);
    }

    [Fact]
    public async Task GetActorById_ShouldReturnActor_WhenGivenValidId()
    {
        var actorId = new ActorId(Guid.NewGuid());
        var actorReadDto = new ActorReadDto(
            actorId,
            new Name("John", null, "Doe"),
            new List<Name>(new [] { new Name("John", "Smith", "Doe")}),
            new DateOnly(1970, 01, 01),
            null
        );

        var request = new GetActorById(actorId);

        _actorReadRepository.Setup(x => x.GetById(actorId).Result)
            .Returns(actorReadDto);

        var actorDto = await _sut.Handle(request, CancellationToken.None);

        actorDto.Should().NotBeNull();
        
        actorDto!.Id.Should().Be(actorId);
        actorDto.Forename.Should().Be("John");
        actorDto.MiddleNames.Should().BeNull();
        actorDto.Surname.Should().Be("Doe");
        actorDto.Name.Should().Be("John Doe");
        actorDto.AlternateNames.Should().Equal("John Smith Doe");
        actorDto.DateOfBirth.Should().Be(new DateOnly(1970, 01, 01));
        actorDto.DateOfDeath.Should().BeNull();
    }
}