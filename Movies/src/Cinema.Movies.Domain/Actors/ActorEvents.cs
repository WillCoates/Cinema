namespace Cinema.Movies.Domain.Actors;

public abstract record ActorEvent(
    ActorId ActorId
);

public record ActorNameChanged(
    ActorId ActorId,
    Name NewName
): ActorEvent(ActorId);

public record ActorDied(
    ActorId ActorId,
    DateTime DateOfDeath
): ActorEvent(ActorId);

public record ActorCreated(
    ActorId ActorId,
    Name Name,
    DateTime DateOfBirth
): ActorEvent(ActorId);