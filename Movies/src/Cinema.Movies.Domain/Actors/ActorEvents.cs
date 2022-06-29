namespace Cinema.Movies.Domain.Actors;

public abstract record ActorEvent(
    ActorId ActorId
);

public record ActorNameChanged(
    ActorId ActorId,
    Name NewName
): ActorEvent(ActorId);
