using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PokeTrackerApi.RunTime.Database;
using PokeTrackerApi.RunTime.Models.Responses;

namespace PokeTrackerApi.RunTime.Endpoints;

public static class PokemonCountsEndpoints
{
    public static void MapPokemonCountsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/pokemonCounts/type", (
                    [FromServices] PokemonRepository repo,
                    [FromServices] IOptions<JsonOptions> jsonOptions) =>
                {
                    var pokemon = repo.GetAll();
                    var counts = pokemon
                        .SelectMany(x => x.Types)
                        .GroupBy(type => type)
                        .Select(x => new GetPokemonCountByTypeResponse { Type = x.Key, Count = x.Count() });

                    return Results.Json(counts, jsonOptions.Value.JsonSerializerOptions);
                }
            )
            .WithName("GetPokemonCountByType");

        app.MapGet("/api/pokemonCounts/generation", (
                    [FromServices] PokemonRepository repo,
                    [FromServices] IOptions<JsonOptions> jsonOptions) =>
                {
                    var pokemon = repo.GetAll();
                    var counts = pokemon
                        .GroupBy(x => x.Generation)
                        .Select(x => new GetPokemonCountByGenerationResponse { Generation = x.Key, Count = x.Count() });

                    return Results.Json(counts, jsonOptions.Value.JsonSerializerOptions);
                }
            )
            .WithName("GetPokemonCountByGeneration");
    }
}
