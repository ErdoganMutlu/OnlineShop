using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Orders;

namespace Api.Managers.Orders.Commands;

internal class OrdersCommandManager : IOrdersCommandManager
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersCommandManager(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Order> AddAsync(int customerId, IEnumerable<int> products)
    {
        var order = new Order
        {
            CustomerId = customerId,
            OrderDateTime = DateTime.Now   //should be UTC
        };
            
        var orderProducts = products.Select(p =>
            new OrderProduct()
            {
                OrderId = order.Id,
                ProductId = p
            }
        ).ToList();

        order.OrderProducts = orderProducts;
            
        _ordersRepository.AddOrUpdate(order);
            
        await _ordersRepository.SaveChangesAsync();

        return order;
    }
}