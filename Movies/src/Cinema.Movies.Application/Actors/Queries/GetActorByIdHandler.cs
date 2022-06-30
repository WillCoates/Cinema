using AutoMapper;
using Cinema.Movies.Application.Actors.Dtos;
using MediatR;

namespace Cinema.Movies.Application.Actors.Queries;

public class GetActorByIdHandler: IRequestHandler<GetActorById, ActorDto?>
{
    private readonly IActorReadRepository _actorRepository;

    public GetActorByIdHandler(IActorReadRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<ActorDto?> Handle(GetActorById request, CancellationToken cancellationToken)
    {
        var actor = await _actorRepository.GetById(request.Id);
        if (actor == null)
        {
            return null;
        }

        var response = new ActorDto
        {
            Id = actor.Id,
            Forename = actor.Name.Forename,
            MiddleNames = actor.Name.MiddleNames,
            Surname = actor.Name.Surname,
            Name = actor.Name.ToString(),
            AlternateNames = actor.AlternativeNames.Select(x => x.ToString()).ToList(),
            DateOfBirth = actor.DateOfBirth,
            DateOfDeath = actor.DateOfDeath
        };

        return response;
    }
}