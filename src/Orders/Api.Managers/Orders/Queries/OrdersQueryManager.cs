using Api.ObjectModels.Repositories.Orders;


namespace Api.Managers.Orders.Queries;

internal class OrdersQueryManager : IOrdersQueryManager
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersQueryManager(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
        
}