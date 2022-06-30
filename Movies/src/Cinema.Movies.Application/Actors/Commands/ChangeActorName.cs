using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public record ChangeActorName(
    ActorId Id,
    string Forename,
    string? MiddleNames,
    string Surname
): IRequest;