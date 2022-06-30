using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Actors;

public interface IActorRepository
{
    public Task Create(Actor actor);
    public Task<Actor?> Read(ActorId id);
    public Task Update(Actor actor);
    public Task Delete(Actor actor);

    public Task<ActorId> NewId();
}