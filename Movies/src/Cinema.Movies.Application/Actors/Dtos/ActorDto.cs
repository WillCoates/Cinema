using Cinema.Movies.Domain.Actors;

namespace Cinema.Movies.Application.Actors.Dtos;

public class ActorDto
{
    public ActorId Id { get; set; } = ActorId.Empty;
    public string Forename { get; set; } = string.Empty;
    public string? MiddleNames { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> AlternateNames { get; set; } = new();
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? DateOfDeath { get; set; }
}
