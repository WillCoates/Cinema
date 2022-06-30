using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Actors.Dtos;

public record ActorReadDto(
    ActorId Id,
    Name Name,
    List<Name> AlternativeNames,
    DateOnly DateOfBirth,
    DateOnly? DateOfDeath
);
    