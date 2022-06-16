using System.Threading.Tasks;
using Api.Controllers;
using Api.Dtos.Products;
using Api.Managers.Products.Commands;
using Api.Managers.Products.Queries;
using Api.ObjectModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceStack;
using Xunit;

namespace Api.UnitTests;

public class ProductsControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();  
    private readonly Mock<IProductsQueryManager> _productsQueryManagerMock = new Mock<IProductsQueryManager>();
    private readonly Mock<IProductsCommandManager> _productsCommandManagerMock = new Mock<IProductsCommandManager>();
    
    [Fact]  
    public async void GetProductById()  
    {  
        // Arrange
        var product = new Product()  
        {  
            Id = 1,  
            Name = "Computer",
            Price = 5000
        };
        var productDto = new ProductDto()  
        {  
            Id = 1,  
            Name = "Computer",
            Price = 5000
        };
        
        // Act
        _productsQueryManagerMock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(product);
        _mapperMock.Setup(p => p.Map<ProductDto>(product)).Returns(productDto);
        
        var controller = new ProductsController(_productsCommandManagerMock.Object,_productsQueryManagerMock.Object, _mapperMock.Object);
        var result = await controller.Get(1);

        // Assert
        Assert.Equal(productDto, ((OkObjectResult)result).Value);
    }  
    
    [Fact]  
    public async void PostProduct()  
    {
        // Arrange
        var createProductDto = new CreateProductDto()  
        {
            Name = "Computer",
            Price = 5000
        };
        
        var product = new Product()  
        {  
            Id = 1,  
            Name = "Computer",
            Price = 5000
        };
        
        // Act
        _mapperMock.Setup(p => p.Map<Product>(createProductDto)).Returns(product);
        _productsCommandManagerMock.Setup(p => p.AddAsync(product)).ReturnsAsync(product);
        
        
        var controller = new ProductsController(_productsCommandManagerMock.Object,_productsQueryManagerMock.Object, _mapperMock.Object);
        var result = await controller.Post(createProductDto);
        
        // Assert
        Assert.Equal(product.Id, ((OkObjectResult)result).Value);
    }  
}