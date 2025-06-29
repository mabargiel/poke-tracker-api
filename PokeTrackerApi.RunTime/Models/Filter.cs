using PokeTrackerApi.RunTime.Enums;

namespace PokeTrackerApi.RunTime.Utils;

public class Filter
{
    public int? Number { get; set; }
    public string? Name { get; set; }
    public PokemonGeneration? Generation { get; set; }
    public PokemonType? Type { get; set; }
}
