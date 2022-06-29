namespace Cinema.Movies.Domain.Actors;

public record Name(
    string Forename,
    string? MiddleNames,
    string Surname
)
{
    public override string ToString()
    {
        if (MiddleNames == null)
        {
            return $"{Forename} {Surname}";
        }

        return $"{Forename} {MiddleNames} {Surname}";
    }
}