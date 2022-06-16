using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Products;
using Api.Tools.Extentions;
using Api.Tools.ObjectModel;

namespace Api.Managers.Products.Queries;

internal class ProductsQueryManager : IProductsQueryManager
{
    private readonly IProductsRepository _productsRepository;

    public ProductsQueryManager(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<PaginatedResponse<Product>> GetProductsByNameAsync(string name, int page, int pageSize)
    {
        Expression<Func<Product, bool>> expression = obj => true;

        expression = expression.And(e => e.Name == name);

        return await _productsRepository.GetWhereAsync(expression, page, pageSize);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _productsRepository.GetByIdAsync(id);
    }
        
    public async Task<IEnumerable<Product>>  GetByIdsAsync(IList<int> entityIds)
    {
        return await _productsRepository.GetByIdsAsync(entityIds);
    }
        
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productsRepository.GetAllAsync();
    }
}