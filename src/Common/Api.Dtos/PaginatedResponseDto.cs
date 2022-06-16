using System.Collections.Generic;

namespace Api.Dtos;

public class PaginatedResponseDto<T> where T : class
{
    public PaginatedResponseDto()
    {
        Facets = new List<FacetDto>();
    }

    public int TotalCount { get; set; }
        
    public IList<T> Items { get; set; }
    public IList<FacetDto> Facets { get; set; }
}

public class FacetDto
{
    public string Name { get; set; }
    public IList<KeyValuePair<string, int>> Values { get; set; }
}