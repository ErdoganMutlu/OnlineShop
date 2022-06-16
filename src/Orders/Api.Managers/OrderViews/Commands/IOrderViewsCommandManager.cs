using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;

namespace Api.Managers.OrderViews.Commands;

public interface IOrderViewsCommandManager
{
    Task<OrderView> AddAsync(OrderView orderView);
    Task<IList<OrderView>> AddRangeAsync(IList<OrderView> orderViews);
}