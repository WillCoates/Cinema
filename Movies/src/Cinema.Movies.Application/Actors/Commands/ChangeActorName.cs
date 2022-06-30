using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public record ChangeActorName(
    ActorId ActorId,
    string Forename,
    string? MiddleNames,
    string Surname
): IRequest;