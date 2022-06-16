namespace Api.Tools.ObjectModel;

public class PaginatedResponse<T> where T : class
{
    public PaginatedResponse()
    {
        TotalCount = 0;
        Items = new List<T>();
        Facets = new List<Facet>();
    }
    public int TotalCount { get; set; }
    public IList<T> Items { get; set; }
    public IList<Facet> Facets { get; set; }
}

public class Facet
{
    public string Name { get; set; }
    public IList<KeyValuePair<string, int>> Values { get; set; }
}