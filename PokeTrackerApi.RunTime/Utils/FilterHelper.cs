using PokeTrackerApi.RunTime.Models;

namespace PokeTrackerApi.RunTime.Utils;

public static class FilterHelper
{
    public static IQueryable<Pokemon> AppleFilter(IQueryable<Pokemon> query, Filter filter)
    {
        if (filter.Number.HasValue)
        {
            query = query.Where(p => p.Number == filter.Number);
        }

        if (filter.Name is not null)
        {
            query = query.Where(p => p.Name.StartsWith(filter.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.Generation is not null)
        {
            query = query.Where(p =>
                p.Generation.ToString().StartsWith(filter.Generation.ToString()!, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.Type is not null)
        {
            query = query.Where(p => p.Types.Contains(filter.Type.Value));
        }

        return query;
    }
}
