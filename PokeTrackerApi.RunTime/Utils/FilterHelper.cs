using PokeTrackerApi.RunTime.Models;

namespace PokeTrackerApi.RunTime.Utils;

public static class FilterHelper
{
    public static IQueryable<Pokemon> AppleFilter(IQueryable<Pokemon> query, Filter filter)
    {
        if (filter.Number is not null)
        {
            if (!int.TryParse(filter.Number, out var number))
            {
                throw new ArgumentException("Number filter must be an integer");
            }

            query = query.Where(p => p.Number == number);
        }

        if (filter.Name is not null)
        {
            query = query.Where(p => p.Name.StartsWith(filter.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.Generation is not null)
        {
            query = query.Where(p =>
                p.Generation.ToString()
                    .Equals(filter.Generation.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.Type is not null)
        {
            query = query.Where(p => p.Types.Contains(filter.Type.Value));
        }

        return query;
    }
}
