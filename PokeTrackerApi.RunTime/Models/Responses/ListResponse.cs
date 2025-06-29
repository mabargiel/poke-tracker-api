using PokeTrackerApi.RunTime.Utils;

namespace PokeTrackerApi.RunTime.Models.Responses;

public class ListResponse<T>(List<T> items, int pageNumber, int pageSize, int totalCount, Sort? sort)
    where T : class
{
    public List<T> Items { get; set; } = items;

    public Pagination Pagination { get; set; } = new()
    {
        PageNumber = pageNumber,
        PageSize = items.Count < pageSize ? items.Count : pageSize,
        TotalItems = totalCount,
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
    };

    public Sort? Sort { get; set; } = sort;
}

public class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}
