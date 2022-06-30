using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Domain.Actors;
using MongoDB.Driver;

namespace Cinema.Movies.Infrastructure.Actors;

public class MongoActorReadRepository: IActorReadRepository
{    
    private readonly MongoClient _mongoClient;
    private readonly IMongoCollection<ActorDocument> _collection;

    public MongoActorReadRepository(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        // TODO: Dump the database in configuration
        _collection = _mongoClient.GetDatabase("movies").GetCollection<ActorDocument>("actors");
    }

    public async Task<ActorReadDto?> GetById(ActorId id)
    {
        var result = await _collection.Find(x => x.Id == id.Id)
            .Project(document => new ActorReadDto(new ActorId(document.Memento.Id), document.Memento.Name!,
                document.Memento.AlternateNames, document.Memento.DateOfBirth, document.Memento.DateOfDeath))
            .SingleOrDefaultAsync();
        return result;
    }
}