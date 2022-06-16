using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;

namespace Api.Managers.Products.Queries;

public interface IProductsQueryManager
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByIdsAsync(IList<int> entityIds);
    Task<PaginatedResponse<Product>> GetProductsByNameAsync(string name, int page, int pageSize);
  
}