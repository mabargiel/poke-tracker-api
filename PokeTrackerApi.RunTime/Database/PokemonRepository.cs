using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using PokeTrackerApi.RunTime.Models;
using PokeTrackerApi.RunTime.Utils;

namespace PokeTrackerApi.RunTime.Database;

public class PokemonRepository
{
    private readonly List<Pokemon> _pokemon;

    public PokemonRepository(IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, "Data", "pokemon.json");
        var json = File.ReadAllText(path);
        _pokemon = JsonSerializer.Deserialize<List<Pokemon>>(json, JsonSettings.Default) ?? [];
    }

    public List<Pokemon> GetAll() => _pokemon;
}
