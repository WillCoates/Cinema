namespace Cinema.Movies.Domain.Actors;

public record struct ActorId(Guid Id)
{
    public static readonly ActorId Empty = new(Guid.Empty);
}