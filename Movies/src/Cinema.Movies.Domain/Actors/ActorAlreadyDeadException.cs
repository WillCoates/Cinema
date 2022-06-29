namespace Cinema.Movies.Domain.Actors;

public class ActorAlreadyDeadException: Exception
{
    public ActorAlreadyDeadException()
    {
    }

    public ActorAlreadyDeadException(string message) : base(message)
    {
    }
}