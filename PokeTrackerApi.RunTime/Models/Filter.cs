using PokeTrackerApi.RunTime.Enums;

namespace PokeTrackerApi.RunTime.Utils;

public class Filter
{
    public string? Number { get; set; }
    public string? Name { get; set; }
    public PokemonGeneration? Generation { get; set; }
    public PokemonType? Type { get; set; }
}
