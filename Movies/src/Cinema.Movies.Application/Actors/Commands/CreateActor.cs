using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public record CreateActor
    (string Forename, string? MiddleName, string Surname, DateTime DateOfBirth) : IRequest<ActorId>;