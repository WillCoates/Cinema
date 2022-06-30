using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public record RegisterActorDeath(
    ActorId ActorId,
    DateOnly DateOfDeath
): IRequest;