using System.Threading.Tasks;
using Api.Dtos.Orders;
using Api.Managers.RabbitMqProducer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRabbitMqProduceService _rabbitMqProduceService;

    public RabbitMqController(IRabbitMqProduceService rabbitMqProduceService,  IMapper mapper)
    {
        _mapper = mapper;
        _rabbitMqProduceService = rabbitMqProduceService;
    }
    
    
    [HttpPost]
    [Route("orders")]
    public async Task<IActionResult> Post(OrderDto orderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _rabbitMqProduceService.SendMessage(orderDto,"Orders");
        
        return Ok();
    }
}