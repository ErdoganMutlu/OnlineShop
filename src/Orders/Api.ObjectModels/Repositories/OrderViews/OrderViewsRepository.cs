using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels.Repositories.OrderViews;

internal class OrderViewsRepository : IOrderViewsRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderViewsRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<PaginatedResponse<OrderView>> GetWhereAsync(Expression<Func<OrderView, bool>> predicate, int page,
        int pageSize)
    {
        var response = new PaginatedResponse<OrderView>();

        var count = _dbContext.OrderViews.Count(predicate);

        var hosts = await _dbContext.OrderViews
            .Where(predicate)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        response.TotalCount = count;
        response.Items = hosts;

        return response;
    }
    
    public OrderView AddOrUpdate(OrderView entity)
    {
        _dbContext.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        return entity;
    }
        
    public IList<OrderView> AddRange(IList<OrderView> entities)
    {
        _dbContext.OrderViews.AddRange(entities);
        return entities;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}