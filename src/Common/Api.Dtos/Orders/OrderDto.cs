using System;
using System.Collections.Generic;

namespace Api.Dtos.Orders;

public class OrderDto
{
    public OrderDto()
    {
        OrderProducts = new List<OrderProductDto>();
    }

    public int Id { get; set; }

    public int CustomerId { get; set; }
    public IList<OrderProductDto> OrderProducts { get; set; }

    public DateTime OrderDateTime { get; set; }
}