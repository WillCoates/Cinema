using Cinema.Movies.Domain.Actors;
using MediatR;

namespace Cinema.Movies.Application.Actors.Commands;

public class RegisterActorDeathHandler: IRequestHandler<RegisterActorDeath>
{
    private readonly IActorRepository _actorRepository;

    public RegisterActorDeathHandler(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<Unit> Handle(RegisterActorDeath request, CancellationToken cancellationToken)
    {
        Actor? actor = await _actorRepository.Read(request.ActorId);

        if (actor == null)
        {
            throw new ActorDoesNotExistException($"Can't find Actor with id {request.ActorId}");
        }
        
        actor.Died(request.DateOfDeath);

        await _actorRepository.Update(actor);
        
        return Unit.Value;
    }
}