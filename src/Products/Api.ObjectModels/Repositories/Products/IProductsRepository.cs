using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools;
using Api.Tools.ObjectModel;

namespace Api.ObjectModels.Repositories.Products;

public interface IProductsRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByIdsAsync(IList<int> entityIds);
    Task<PaginatedResponse<Product>> GetWhereAsync(Expression<Func<Product, bool>> predicate, int page, int pageSize);
}