using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos.Orders;

namespace Api.OrderServiceClients.OrderViews;

public interface IOrderViewsClientService
{
    Task<bool> AddRange(IList<OrderViewDto> orderViewDtos);
}