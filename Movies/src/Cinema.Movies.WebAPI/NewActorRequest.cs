class NewActorRequest
{
    public string Forename { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string Surname { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}