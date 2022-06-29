namespace Cinema.Movies.Domain.Actors;

public class Actor
{
    public Actor(ActorId id, Name name, DateOnly dateOfBirth)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        
        _events.Add(new ActorCreated(id, name, dateOfBirth));
    }
    
    public ActorId Id { get; }
    public Name Name { get; private set; }

    private List<Name> _alternateNames = new();
    public IReadOnlyList<Name> AlternateNames => _alternateNames.AsReadOnly();

    private List<ActorEvent> _events = new();
    public IReadOnlyList<ActorEvent> Events => _events.AsReadOnly();

    public DateOnly DateOfBirth { get; }
    
    public DateOnly? DateOfDeath { get; private set; }

    public void ChangeName(Name newName)
    {
        if (!_alternateNames.Contains(Name))
        {
            _alternateNames.Add(Name);
        }

        Name = newName;
        
        _events.Add(new ActorNameChanged(Id, newName));
    }

    public void Died(DateOnly dateOfDeath)
    {
        if (DateOfDeath != null)
        {
            throw new ActorAlreadyDeadException("Actor already died");
        }
        
        DateOfDeath = dateOfDeath;
        
        _events.Add(new ActorDied(Id, dateOfDeath));
    }
}