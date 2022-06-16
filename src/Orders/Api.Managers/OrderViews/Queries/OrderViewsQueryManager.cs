using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Orders;
using Api.ObjectModels.Repositories.OrderViews;
using Api.Tools.Extentions;
using Api.Tools.ObjectModel;

namespace Api.Managers.OrderViews.Queries;

internal class OrderViewsQueryManager : IOrderViewsQueryManager
{
    private readonly IOrderViewsRepository _orderViewsRepository;

    public OrderViewsQueryManager(IOrderViewsRepository orderViewsRepository)
    {
        _orderViewsRepository = orderViewsRepository;
    }
    
    public async Task<PaginatedResponse<OrderView>> FilterOrderViews(DateTime from, DateTime to, int page, int pageSize)
    {
        Expression<Func<OrderView, bool>> expression = obj => true;

        expression = expression.And(e => e.OrderDateTime > from);
        expression = expression.And(e => e.OrderDateTime < to);

        return await _orderViewsRepository.GetWhereAsync(expression, page, pageSize);
    }
        
}