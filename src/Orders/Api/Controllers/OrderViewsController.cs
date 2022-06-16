using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.Orders;
using Api.Managers.OrderViews.Commands;
using Api.Managers.OrderViews.Queries;
using Api.ObjectModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderViewsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderViewsQueryManager _orderViewsQueryManager;
    private readonly IOrderViewsCommandManager _orderViewsCommandManager;


    public OrderViewsController(IOrderViewsCommandManager orderViewsCommandManager,
        IOrderViewsQueryManager orderViewsQueryManager, IMapper mapper)
    {
        _mapper = mapper;
        _orderViewsQueryManager = orderViewsQueryManager;
        _orderViewsCommandManager = orderViewsCommandManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] OrderFilterDto filters, int page = 1, int pageSize = 20)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var orderViews = await _orderViewsQueryManager.FilterOrderViews(filters.From, filters.To, page, pageSize);

        var orderViewsDto = _mapper.Map<PaginatedResponseDto<OrderViewDto>>(orderViews);

        return Ok(orderViewsDto);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(IList<OrderViewDto> orderViewDtos)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var orderViews = _mapper.Map<IList<OrderView>>(orderViewDtos);

        orderViews = await _orderViewsCommandManager.AddRangeAsync(orderViews);

        return Ok(orderViews.Select(a => a.Id).ToList());
    }
}