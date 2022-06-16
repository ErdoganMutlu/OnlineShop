using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools;
using Api.Tools.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels.Repositories.Customers;

internal class CustomersRepository : ICustomersRepository
{
    private readonly CustomerDbContext _dbContext;

    public CustomersRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbContext.Customers
            .ToListAsync();
    }

    public async Task<PaginatedResponse<Customer>> GetWhereAsync(Expression<Func<Customer, bool>> predicate, int page,
        int pageSize)
    {
        var response = new PaginatedResponse<Customer>();
        var count = _dbContext.Customers.Count(predicate);

        var hosts = await _dbContext.Customers
            .Where(predicate)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        response.TotalCount = count;
        response.Items = hosts;

        return response;
    }


    public async Task<Customer> GetByIdAsync(int id)
    {
        var result = await this.FindAsync(id);

        return result;
    }

    public async Task<Customer> FindAsync(int id)
    {
        Ensure.IsValidId(id);

        return await _dbContext.Customers
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public Customer AddOrUpdate(Customer entity)
    {
        _dbContext.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;

        return entity;
    }

    public void Delete(int id)
    {
        var entity = GetByIdAsync(id).Result;
        _dbContext.Customers.Remove(entity);
    }

    public void Delete(Customer entity)
    {
        _dbContext.Customers.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}