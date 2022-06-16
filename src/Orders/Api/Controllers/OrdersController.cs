using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.Orders;
using Api.Managers.Orders.Commands;
using Api.Managers.Orders.Queries;
using Api.ObjectModels.Entities;
using Api.RabbitMqServiceClients.RabbitMq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrdersCommandManager _ordersCommandManager;
    private readonly IRabbitMqClientService _rabbitMqClientService;
    
    public OrdersController(IOrdersCommandManager ordersCommandManager, IRabbitMqClientService rabbitMqClientService, IMapper mapper)
    {
        _mapper = mapper;
        _rabbitMqClientService = rabbitMqClientService;
        _ordersCommandManager = ordersCommandManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderDto createOrderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
            
        var order = await _ordersCommandManager.AddAsync(createOrderDto.CustomerId, createOrderDto.Products);
        
        var orderDto = _mapper.Map<OrderDto>(order);
        
        await _rabbitMqClientService.SendOrder(orderDto);  // it depends on scenario we don't need to wait in some cases
        
        return Ok(order.Id);
    }
        
        
}