using System;
using System.Collections.Generic;
using Api.Controllers;
using Api.Dtos;
using Api.Dtos.Orders;
using Api.Managers.OrderViews.Commands;
using Api.Managers.OrderViews.Queries;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests;

public class OrderViewControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IOrderViewsQueryManager> _orderViewsQueryManager = new Mock<IOrderViewsQueryManager>();

    private readonly Mock<IOrderViewsCommandManager> _orderViewCommandManagerMock =
        new Mock<IOrderViewsCommandManager>();

    [Fact]
    public async void PostOrderViews()
    {
        // Arrange
        var orderViewsDto = new List<OrderViewDto>()
        {
            new OrderViewDto()
            {
                Id = 1,
                CustomerName = "Ahmet"
            }
        };

        var orderViews = new List<OrderView>()
        {
            new OrderView()
            {
                Id = 1,
                CustomerName = "Ahmet"
            }
        };

        // Act
        _mapperMock.Setup(p => p.Map<IList<OrderView>>(orderViewsDto)).Returns(orderViews);
        _orderViewCommandManagerMock.Setup(p => p.AddRangeAsync(orderViews)).ReturnsAsync(orderViews);

        var controller = new OrderViewsController(_orderViewCommandManagerMock.Object, _orderViewsQueryManager.Object,
            _mapperMock.Object);
        var result = await controller.Post(orderViewsDto);

        // Assert
        Assert.Equal(new List<int>() {1}, ((OkObjectResult) result).Value);
    }

    [Fact]
    public async void GetOrderViews()
    {
        // Arrange
        var filters = new OrderFilterDto()
        {
            From = DateTime.Now.AddDays(-2),
            To = DateTime.Now
        };

        var orderViews = new PaginatedResponse<OrderView>()
        {
            TotalCount = 2,
            Items = new List<OrderView>()
            {
                new OrderView()
                {
                    Id = 1,
                    CustomerName = "Ahmet"
                },
                new OrderView()
                {
                    Id = 2,
                    CustomerName = "Mehmet"
                }
            }
        };
        var orderViewsDto = new PaginatedResponseDto<OrderViewDto>()
        {
            TotalCount = 2,
            Items = new List<OrderViewDto>()
            {
                new OrderViewDto()
                {
                    Id = 1,
                    CustomerName = "Ahmet"
                },
                new OrderViewDto()
                {
                    Id = 2,
                    CustomerName = "Mehmet"
                }
            }
        };

        // Act
        _mapperMock.Setup(p => p.Map<PaginatedResponseDto<OrderViewDto>>(orderViews)).Returns(orderViewsDto);
        _orderViewsQueryManager.Setup(p => p.FilterOrderViews(filters.From, filters.To, 1, 20))
            .ReturnsAsync(orderViews);

        var controller = new OrderViewsController(_orderViewCommandManagerMock.Object, _orderViewsQueryManager.Object,
            _mapperMock.Object);
        var result = await controller.Get(filters);

        // Assert
        Assert.Equal(orderViewsDto, ((OkObjectResult) result).Value);
    }
}