using Microsoft.AspNetCore.Mvc;
using PokeTrackerApi.RunTime.Database;
using PokeTrackerApi.RunTime.Models.Responses;

namespace PokeTrackerApi.RunTime.Endpoints;

public static class PokemonCountsEndpoints
{
    public static void MapPokemonCountsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/pokemonCounts/type", ([FromServices] PokemonRepository repo) =>
                {
                    var pokemon = repo.GetAll();
                    var counts = pokemon
                        .SelectMany(x => x.Types)
                        .GroupBy(type => type)
                        .Select(x => new GetPokemonCountByTypeResponse { Type = x.Key, Count = x.Count() });

                    return Results.Json(counts);
                }
            )
            .WithName("GetPokemonCountByType");

        app.MapGet("/api/pokemonCounts/generation", ([FromServices] PokemonRepository repo) =>
                {
                    var pokemon = repo.GetAll();
                    var counts = pokemon
                        .GroupBy(x => x.Generation)
                        .Select(x => new GetPokemonCountByGenerationResponse { Generation = x.Key, Count = x.Count() });

                    return Results.Json(counts);
                }
            )
            .WithName("GetPokemonCountByGeneration");
    }
}
