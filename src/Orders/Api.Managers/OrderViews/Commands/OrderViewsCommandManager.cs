using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.OrderViews;

namespace Api.Managers.OrderViews.Commands;

internal class OrderViewsCommandManager : IOrderViewsCommandManager
{
    private readonly IOrderViewsRepository _orderViewsRepository;

    public OrderViewsCommandManager(IOrderViewsRepository orderViewsRepository)
    {
        _orderViewsRepository = orderViewsRepository;
    }
    
    public async Task<OrderView> AddAsync(OrderView orderView)
    {
        _orderViewsRepository.AddOrUpdate(orderView);
            
        await _orderViewsRepository.SaveChangesAsync();

        return orderView;
    }
    
    public async Task<IList<OrderView>> AddRangeAsync(IList<OrderView> orderViews)
    {
        _orderViewsRepository.AddRange(orderViews);
            
        await _orderViewsRepository.SaveChangesAsync();

        return orderViews;
    }
}