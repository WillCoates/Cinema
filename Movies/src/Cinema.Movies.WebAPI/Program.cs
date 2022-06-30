using Cinema.Movies.Application.Actors;
using Cinema.Movies.Application.Actors.Commands;
using Cinema.Movies.Application.Actors.Dtos;
using Cinema.Movies.Application.Actors.Queries;
using Cinema.Movies.Domain.Actors;
using Cinema.Movies.Infrastructure.Actors;
using MongoDB.Driver;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new MongoClient("mongodb://movies-db"));
builder.Services.AddScoped<IActorRepository, MongoActorRepository>();
builder.Services.AddScoped<IActorReadRepository, MongoActorReadRepository>();
builder.Services.AddMediatR(typeof(CreateActorHandler));

var app = builder.Build();

app.MapGet("/actors/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var actorId = new ActorId(id);
    var request = new GetActorById(actorId);
    ActorDto? response = await mediator.Send(request);
    return response == null ? Results.NotFound() : Results.Ok(response);
});

app.MapPost("/actors/", async (NewActorRequest body, IMediator mediator) =>
{
    var request = new CreateActor(
        body.Forename,
        body.MiddleName,
        body.Surname,
        body.DateOfBirth
    );
    ActorId response = await mediator.Send(request);
    return Results.Created($"/actors/{response.Id}", null);
});

app.Run("http://0.0.0.0:8080");