using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.Products;
using Api.Managers.Products.Commands;
using Api.Managers.Products.Queries;
using Api.ObjectModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductsQueryManager _productsQueryManager;
    private readonly IProductsCommandManager _productsCommandManager;
        
    public ProductsController(IProductsCommandManager productsCommandManager, IProductsQueryManager productsQueryManager, IMapper mapper)
    {
        _mapper = mapper;
        _productsQueryManager = productsQueryManager;
        _productsCommandManager = productsCommandManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productsQueryManager.GetAllAsync();
        
        return Ok(_mapper.Map<IList<ProductDto>>(products));
    }
        
    [HttpGet]
    [Route("name/{name}")]
    public async Task<IActionResult> GetProductsByName(string name, int page = 1, int pageSize = 100)
    {
        var products = await _productsQueryManager.GetProductsByNameAsync(name, page, pageSize);
        var productsDto = _mapper.Map<PaginatedResponseDto<ProductDto>>(products);
        return Ok(productsDto);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productsQueryManager.GetByIdAsync(id);

        if (product is null) return NotFound();

        var dto = _mapper.Map<ProductDto>(product);
        return Ok(dto);
    }
        
    [HttpGet]
    [Route("GetByIds")]
    public async Task<IActionResult> GetByIds([FromQuery]int[] productIds)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var products = await _productsQueryManager.GetByIdsAsync(productIds);
        if (products is null) return NotFound();
        var productsDto = _mapper.Map<IList<ProductDto>>(products);
            
        return Ok(productsDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var host = _mapper.Map<Product>(productDto);

        host = await _productsCommandManager.AddAsync(host);

        return Ok(host.Id);
    }
        
    [HttpPut]
    public async Task<IActionResult> Put(ProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var product = _mapper.Map<Product>(productDto);

        product = await _productsCommandManager.UpdateAsync(product);

        return Ok(product.Id);
    }
        
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var host = await _productsQueryManager.GetByIdAsync(id);

        if (host is null) return NotFound();
            
        var result = await _productsCommandManager.DeleteAsync(id);

        return Ok(result);
    }
}