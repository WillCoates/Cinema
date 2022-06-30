using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Actors;

public interface IActorReadRepository
{
    Task<ActorReadDto?> GetById(ActorId id);
}