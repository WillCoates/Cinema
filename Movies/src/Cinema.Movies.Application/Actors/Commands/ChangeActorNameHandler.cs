using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public class ChangeActorNameHandler: IRequestHandler<ChangeActorName>
{
    private readonly IActorRepository _actorRepository;

    public ChangeActorNameHandler(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<Unit> Handle(ChangeActorName request, CancellationToken cancellationToken)
    {
        Actor? actor = await _actorRepository.Read(request.ActorId);

        if (actor == null)
        {
            throw new ActorDoesNotExistException($"Can't find Actor with id {request.ActorId}");
        }
        
        actor.ChangeName(new Name(request.Forename, request.MiddleNames, request.Surname));

        await _actorRepository.Update(actor);
        
        return Unit.Value;
    }
}