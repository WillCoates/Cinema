using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Queries;

public record GetActorById(
    ActorId Id
): IRequest<ActorDto?>;