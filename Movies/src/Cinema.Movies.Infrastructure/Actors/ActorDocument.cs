using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Infrastructure.Actors;

public class ActorDocument
{
    public Guid Id { get; set; }
    public long Version { get; set; }
    public ActorMemento Memento { get; set; } = null!;
}