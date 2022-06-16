using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Orders;
using Api.Tools;
using Api.Tools.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels.Repositories.Orders;

internal class OrdersRepository : IOrdersRepository
{
    private readonly OrderDbContext _dbContext;

    public OrdersRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<PaginatedResponse<Order>> GetWhereAsync(Expression<Func<Order, bool>> predicate, int page,
        int pageSize)
    {
        var response = new PaginatedResponse<Order>();

        var count = _dbContext.Orders.Count(predicate);

        var hosts = await _dbContext.Orders
            .Where(predicate)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        response.TotalCount = count;
        response.Items = hosts;

        return response;
    }
        
    public Order AddOrUpdate(Order entity)
    {
        _dbContext.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;

        _dbContext.OrderProducts.AddRange(entity.OrderProducts);
            
        return entity;
    }
        
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}