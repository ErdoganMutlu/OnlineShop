using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Orders;
using Api.IntegrationTests.Base;
using FluentAssertions;
using Newtonsoft.Json;
using ServiceStack;
using Xunit;

namespace Api.IntegrationTests;

public class ServiceProvidersControllerTests : BaseClassFixture
{
    public ServiceProvidersControllerTests(TestFixture fixture) : base(fixture)
    {
    }

    [Fact, Priority(1)]
    public async Task Post_Order_Without_Products_Fail()
    {
        // Arrange
        var request = new
        {
            Url = "api/orders",
            Body = new
            {
                customerId = 1
            }
        };

        // Act
        var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    //!!!RabbitMqService must be running!!!
    // [Fact, Priority(2)]
    // public async Task Post_Order_Success()
    // {
    //     // Arrange
    //     var request = new
    //     {
    //         Url = "api/orders",
    //         Body = new
    //         {
    //             customerId = 1,
    //             products = new List<int>()
    //             {
    //                 1
    //             }
    //         }
    //     };
    //
    //     // Act
    //     var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
    //     
    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    // }


    [Fact, Priority(4)]
    public async Task Get_Order_Fail_NotFound()
    {
        // Arrange
        var request = new
        {
            Url = $"api/orders/-1"
        };

        // Act
        var response = await Client.GetAsync(request.Url);
        var responseData = await response.Content.ReadAsStringAsync();
        var responseModel = JsonConvert.DeserializeObject<OrderDto>(responseData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}