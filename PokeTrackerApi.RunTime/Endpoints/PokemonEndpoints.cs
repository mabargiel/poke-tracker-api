using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PokeTrackerApi.RunTime.Database;
using PokeTrackerApi.RunTime.Models.Responses;
using PokeTrackerApi.RunTime.Utils;

namespace PokeTrackerApi.RunTime.Endpoints;

public static class PokemonEndpoints
{
    public static void MapPokemonEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/pokemon", (
                [FromServices] PokemonRepository repo,
                [FromServices] IOptions<JsonOptions> jsonOptions,
                [FromQuery(Name = "page_number")] int pageNumber,
                [FromQuery(Name = "page_size")] int pageSize,
                string? filter,
                string? sort) =>
            {
                var pokemon = repo.GetAll();

                if (pokemon.Count == 0)
                {
                    return Results.NoContent();
                }

                if (pageNumber < 1 || pageSize < 1)
                {
                    return Results.BadRequest("Invalid page number or page size");
                }

                try
                {
                    var filterObject = filter != null
                        ? JsonSerializer.Deserialize<Filter>(filter, JsonSettings.Default)
                        : null;
                    var sortObject = sort != null
                        ? JsonSerializer.Deserialize<Sort>(sort, JsonSettings.Default)
                        : null;

                    var filtered = pokemon.AsQueryable();
                    if (filterObject is not null)
                    {
                        filtered = FilterHelper.AppleFilter(filtered, filterObject);
                    }

                    if (sortObject is not null)
                    {
                        filtered = SortHelper.ApplySorting(filtered, sortObject);
                    }

                    var totalCount = filtered.Count();
                    filtered = filtered.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                    return Results.Json(new GetPokemonCollectionResponse([.. filtered], pageNumber, pageSize,
                        totalCount,
                        sortObject), jsonOptions.Value.JsonSerializerOptions);
                }
                catch (JsonException)
                {
                    return Results.BadRequest("Could not parse filter or sort or both");
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex);
                }
            })
            .WithName("GetPokemonCollection");

        app.MapGet("/api/pokemon/{number}", (
                    [FromServices] PokemonRepository repo,
                    [FromServices] IOptions<JsonOptions> jsonOptions, string number) =>
                {
                    var pokemon = repo.GetAll();

                    if (!int.TryParse(number, out var numberValue))
                    {
                        return Results.BadRequest("Invalid pokemon number");
                    }

                    var pokemonDetails = pokemon.FirstOrDefault(x => x.Number == numberValue);

                    return pokemonDetails == null
                        ? Results.NotFound($"Pokemon with number {numberValue} not found")
                        : Results.Json(pokemonDetails, jsonOptions.Value.JsonSerializerOptions);
                }
            )
            .WithName("GetPokemon");
    }
}
