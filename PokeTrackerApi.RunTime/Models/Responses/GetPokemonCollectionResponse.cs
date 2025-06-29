using PokeTrackerApi.RunTime.Utils;

namespace PokeTrackerApi.RunTime.Models.Responses;

public class GetPokemonCollectionResponse(List<Pokemon> items, int pageNumber, int pageSize, int totalCount, Sort? sort)
    : ListResponse<Pokemon>(items, pageNumber, pageSize, totalCount, sort)
{
}
