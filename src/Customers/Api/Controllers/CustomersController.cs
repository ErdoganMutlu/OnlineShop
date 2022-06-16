using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.Customers;
using Api.Managers.Customers.Commands;
using Api.Managers.Customers.Queries;
using Api.ObjectModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomersQueryManager _customersQueryManager;
    private readonly ICustomersCommandManager _customersCommandManager;
        
    public CustomersController(ICustomersCommandManager customersCommandManager, ICustomersQueryManager customersQueryManager, IMapper mapper)
    {
        _mapper = mapper;
        _customersQueryManager = customersQueryManager;
        _customersCommandManager = customersCommandManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _customersQueryManager.GetAllAsync();

        return Ok(_mapper.Map<IList<CustomerDto>>(customers));
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<IActionResult> GetCustomersByName(string name, int page = 1, int pageSize = 100)
    {
        var customers = await _customersQueryManager.GetCustomersByNameAsync(name, page, pageSize);
        var customersDto = _mapper.Map<PaginatedResponseDto<CustomerDto>>(customers);
        return Ok(customersDto);
    }
        
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _customersQueryManager.GetByIdAsync(id);

        if (customer is null) return NotFound();

        var dto = _mapper.Map<CustomerDto>(customer);
        return Ok(dto);
    }
        
    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerDto customerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var host = _mapper.Map<Customer>(customerDto);

        host = await _customersCommandManager.AddAsync(host);

        return Ok(host.Id);
    }
        
    [HttpPut]
    public async Task<IActionResult> Put(CustomerDto customerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var customer = _mapper.Map<Customer>(customerDto);

        customer = await _customersCommandManager.UpdateAsync(customer);

        return Ok(customer.Id);
    }
        
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var host = await _customersQueryManager.GetByIdAsync(id);

        if (host is null) return NotFound();
            
        var result = await _customersCommandManager.DeleteAsync(id);

        return Ok(result);
    }
}