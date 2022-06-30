namespace Cinema.Movies.Application.Actors;

public class ActorDoesNotExistException : Exception
{
    public ActorDoesNotExistException()
    {
    }
    
    public ActorDoesNotExistException(string message): base(message)
    {
    }
}