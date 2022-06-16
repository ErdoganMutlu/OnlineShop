using Api.Controllers;
using Api.Dtos.Orders;
using Api.Managers.RabbitMqProducer;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests;

public class RabbitMqControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();  
    private readonly Mock<IRabbitMqProduceService> _rabbitMqProduceService = new Mock<IRabbitMqProduceService>();
    
    [Fact]  
    public async void PostRabbitMq()  
    {
        // Arrange
        var orderDto = new OrderDto()  
        {
            
        };
        
        // Act

        var controller = new RabbitMqController(_rabbitMqProduceService.Object, _mapperMock.Object);
        var response = await controller.Post(orderDto);
        
        // Assert
        response.Should().BeOfType<OkResult>();
    }  
}