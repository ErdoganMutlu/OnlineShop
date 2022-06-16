using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools;
using Api.Tools.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels.Repositories.Products;

internal class ProductsRepository : IProductsRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductsRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products
            .ToListAsync();
    }

    public async Task<PaginatedResponse<Product>> GetWhereAsync(Expression<Func<Product, bool>> predicate, int page,
        int pageSize)
    {
        var response = new PaginatedResponse<Product>();
        var count = _dbContext.Products.Count(predicate);

        var hosts = await _dbContext.Products
            .Where(predicate)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        response.TotalCount = count;
        response.Items = hosts;

        return response;
    }


    public async Task<Product> GetByIdAsync(int id)
    {
        var result = await this.FindAsync(id);

        return result;
    }

    public async Task<IEnumerable<Product>> GetByIdsAsync(IList<int> entityIds)
    {
        var products =  await _dbContext.Products
            .Where(t => entityIds.Contains(t.Id)).ToListAsync();
            
        return products;
    }
        
    public async Task<Product> FindAsync(int id)
    {
        Ensure.IsValidId(id);

        return await _dbContext.Products
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public Product AddOrUpdate(Product entity)
    {
        _dbContext.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;

        return entity;
    }

    public void Delete(int id)
    {
        var entity = GetByIdAsync(id).Result;
        _dbContext.Products.Remove(entity);
    }

    public void Delete(Product entity)
    {
        _dbContext.Products.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}