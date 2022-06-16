using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Dtos.Orders;
using Api.Managers.Orders.Commands;
using Api.ObjectModels.Entities;
using Api.RabbitMqServiceClients.RabbitMq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests;

public class OrdersControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IOrdersCommandManager> _ordersCommandManagerMock = new Mock<IOrdersCommandManager>();
    private readonly Mock<IRabbitMqClientService> _rabbitMqClientService = new Mock<IRabbitMqClientService>();

    [Fact]
    public async void PostOrder()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto()
        {
            CustomerId = 1,
            Products = new List<int>() {1, 2}
        };

        var order = new Order()
        {
            Id = 5,
            CustomerId = 1,
            OrderProducts = new List<OrderProduct>()
            {
                new OrderProduct(){ OrderId = 1, ProductId = 1},
                new OrderProduct(){ OrderId = 1, ProductId = 2},
            },
            OrderDateTime = new DateTime(2016,6,28)
        };

        // Act
        _mapperMock.Setup(p => p.Map<Order>(createOrderDto)).Returns(order);
        _ordersCommandManagerMock.Setup(p => p.AddAsync(createOrderDto.CustomerId, createOrderDto.Products))
            .ReturnsAsync(order);


        var controller = new OrdersController(_ordersCommandManagerMock.Object, _rabbitMqClientService.Object,
            _mapperMock.Object);
        var result = await controller.Post(createOrderDto);

        // Assert
        Assert.Equal(5, ((OkObjectResult)result).Value);
        Assert.Equal(order.OrderDateTime, new DateTime(2016,6,28));
        
        
    }
}