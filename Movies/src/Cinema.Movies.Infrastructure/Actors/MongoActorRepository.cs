using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Domain.Actors;
using MongoDB.Driver;

namespace Cinema.Movies.Infrastructure.Actors;

public class MongoActorRepository: IActorRepository
{
    // MongoDB clients are thread-safe and are recommended to store directly in IoC container
    // see https://mongodb.github.io/mongo-csharp-driver/2.16/reference/driver/connecting/#re-use
    private readonly MongoClient _mongoClient;
    private readonly IMongoCollection<ActorDocument> _collection;

    // Domain objects shouldn't contain their version
    private readonly Dictionary<Actor, long> _versions = new();

    public MongoActorRepository(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        // TODO: Dump the database in configuration
        _collection = _mongoClient.GetDatabase("movies").GetCollection<ActorDocument>("actors");
    }
    
    public async Task Create(Actor actor)
    {
        var document = new ActorDocument()
        {
            Id = actor.Id.Id,
            Version = 0,
            Memento = actor.GetMemento()
        };
        await _collection.InsertOneAsync(document);
    }

    public async Task<Actor?> Read(ActorId id)
    {
        var cursor = await _collection.FindAsync(x => x.Id == id.Id);
        var document = await cursor.SingleOrDefaultAsync();
        if (document == null)
        {
            return null;
        }

        var actor = new Actor(document.Memento);
        _versions[actor] = document.Version;
        return actor;
    }

    public async Task Update(Actor actor)
    {
        var version = _versions[actor];
        var document = new ActorDocument()
        {
            Id = actor.Id.Id,
            Version = version + 1,
            Memento = actor.GetMemento()
        };
        var result = await _collection.FindOneAndReplaceAsync(x => x.Id == actor.Id.Id && x.Version == version, document);

        if (result == null)
        {
            throw new ActorChangedException();
        }
    }

    public async Task Delete(Actor actor)
    {
        await _collection.DeleteOneAsync(x => x.Id == actor.Id.Id);
        _versions.Remove(actor);
    }

    public Task<ActorId> NewId()
    {
        return Task.FromResult(new ActorId(Guid.NewGuid()));
    }
}