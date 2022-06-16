using System;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Products;
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
    public async Task Post_Product_Without_Name_Fail()
    {
        // Arrange
        var request = new
        {
            Url = "api/customers",
            Body = new
            {
                id = 0,
                surname = "Mutlu",
                details = "Software"
            }
        };
        
        // Act
        var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        var responseData = await response.Content.ReadAsStringAsync();
        var responseModel = JsonConvert.DeserializeObject<ProductDto>(responseData);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
        
        
    [Fact, Priority(2)]
    public async Task Post_Product_Success()
    {
        // Arrange
        var request = new
        {
            Url = "api/customers",
            Body = new
            {
                id = 0,
                name = "Erdogan",
                surname = "Mutlu",
                details = "Software"
            }
        };
        
        // Act
        var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
        
    [Fact, Priority(4)]
    public async Task Get_Product_NotFound()
    {
        // Arrange
        var request = new
        {
            Url = $"api/customers/10000000"
        };
        
        // Act
        var response = await Client.GetAsync(request.Url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
        
    
    [Fact, Priority(4)]
    public async Task Get_Product_Error()
    {
        // Arrange
        var request = new
        {
            Url = $"api/customers/-1"
        };
        
        // Act
        var response = await Client.GetAsync(request.Url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
        
    [Fact]
    public async Task Get_ProductsByName_Success()
    {
        // Arrange
        var request = "api/customers/name/erdogan";

        // Act
        var response = await Client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}