using PokeTrackerApi.RunTime.Enums;

namespace PokeTrackerApi.RunTime.Models.Responses;

public class GetPokemonCountByTypeResponse
{
    public required PokemonType Type { get; set; }
    public required int Count { get; set; }
}

public class GetPokemonCountByGenerationResponse
{
    public required PokemonGeneration Generation { get; set; }
    public required int Count { get; set; }
}
