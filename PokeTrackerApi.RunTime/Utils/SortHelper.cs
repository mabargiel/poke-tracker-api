using PokeTrackerApi.RunTime.Enums;
using PokeTrackerApi.RunTime.Models;

namespace PokeTrackerApi.RunTime.Utils;

public static class SortHelper
{
    public static IQueryable<Pokemon> ApplySorting(IQueryable<Pokemon> query, Sort sort)
    {
        var validKeys = new[]
            { "number", "name", "generation", "height", "weight", "type", "moves count", "movescount" };

        if (!validKeys.Contains(sort.Key))
        {
            throw new ArgumentException($"Invalid sort key: {sort.Key}. Valid keys are: {string.Join(",", validKeys)}");
        }

        var isDescending = sort.Direction == Direction.Desc;

        return sort.Key.ToLower() switch
        {
            "number" => isDescending ? query.OrderByDescending(p => p.Number) : query.OrderBy(p => p.Number),
            "name" => isDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "generation" => isDescending
                ? query.OrderByDescending(p => p.Generation)
                : query.OrderBy(p => p.Generation),
            "height" => isDescending ? query.OrderByDescending(p => p.Height) : query.OrderBy(p => p.Height),
            "weight" => isDescending ? query.OrderByDescending(p => p.Weight) : query.OrderBy(p => p.Weight),
            "type" => isDescending
                ? query.OrderByDescending(p => p.Types.FirstOrDefault())
                : query.OrderBy(p => p.Types.FirstOrDefault()),
            "movescount" => isDescending
                ? query.OrderByDescending(p => p.Moves.Count)
                : query.OrderBy(p => p.Moves.Count),
            _ => query
        };
    }
}
