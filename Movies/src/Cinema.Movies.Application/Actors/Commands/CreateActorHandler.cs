using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public class CreateActorHandler: IRequestHandler<CreateActor, ActorId>
{
    private readonly IActorRepository _actorRepository;

    public CreateActorHandler(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<ActorId> Handle(CreateActor request, CancellationToken cancellationToken)
    {
        ActorId actorId = await _actorRepository.NewId();
        Name actorName = new Name(request.Forename, request.MiddleName, request.Surname);
        Actor actor = new Actor(actorId, actorName, request.DateOfBirth);

        await _actorRepository.Create(actor);

        return actorId;
    }
}