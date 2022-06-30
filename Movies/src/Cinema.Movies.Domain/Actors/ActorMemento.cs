namespace Cinema.Movies.Domain.Actors;

public class ActorMemento
{
    public Guid Id { get; set; }
    public Name? Name { get; set; }
    public List<Name> AlternateNames { get; set; } = new();
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
}