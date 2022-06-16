using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;

namespace Api.ObjectModels.Repositories.OrderViews;

public interface IOrderViewsRepository
{
    Task<PaginatedResponse<OrderView>> GetWhereAsync(Expression<Func<OrderView, bool>> predicate, int page,
        int pageSize);
    OrderView AddOrUpdate(OrderView entity);
    IList<OrderView> AddRange(IList<OrderView> entities);
    Task SaveChangesAsync();
}