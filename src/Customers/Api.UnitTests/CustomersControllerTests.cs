using Api.Controllers;
using Api.Dtos.Customers;
using Api.Managers.Customers.Commands;
using Api.Managers.Customers.Queries;
using Api.ObjectModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests;

public class CustomersControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();  
    private readonly Mock<ICustomersQueryManager> _customersQueryManagerMock = new Mock<ICustomersQueryManager>();
    private readonly Mock<ICustomersCommandManager> _customersCommandManagerMock = new Mock<ICustomersCommandManager>();
    
    [Fact]  
    public async void GetCustomerById()  
    {  
        // Arrange
        var customer = new Customer()  
        {  
            Id = 1,  
            Name = "Erdogan",
            Surname = "Mutlu"
        };
        var customerDto = new CustomerDto()  
        {  
            Id = 1,  
            Name = "Computer",
            Surname = "Mutlu"
        };
        
        // Act
        _customersQueryManagerMock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(customer);
        _mapperMock.Setup(p => p.Map<CustomerDto>(customer)).Returns(customerDto);
        
        var controller = new CustomersController(_customersCommandManagerMock.Object,_customersQueryManagerMock.Object, _mapperMock.Object);
        var result = await controller.Get(1);

        // Assert
        Assert.Equal(customerDto, ((OkObjectResult)result).Value);
    }  
    
    [Fact]  
    public async void PostCustomer()  
    {
        // Arrange
        var createCustomerDto = new CreateCustomerDto()  
        {
            Name = "Erdogan",
            Surname = "Mutlu"
        };
        
        var customer = new Customer()  
        {  
            Id = 1,  
            Name = "Erdogan",
            Surname = "Mutlu"
        };
        
        // Act
        _mapperMock.Setup(p => p.Map<Customer>(createCustomerDto)).Returns(customer);
        _customersCommandManagerMock.Setup(p => p.AddAsync(customer)).ReturnsAsync(customer);
        
        
        var controller = new CustomersController(_customersCommandManagerMock.Object,_customersQueryManagerMock.Object, _mapperMock.Object);
        var result = await controller.Post(createCustomerDto);
        
        // Assert
        Assert.Equal(customer.Id, ((OkObjectResult)result).Value);
    }  
}