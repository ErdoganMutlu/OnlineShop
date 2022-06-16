using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Api.UnitTests.Base;

public class BaseClassFixture : IClassFixture<TestFixture>
{
    protected readonly HttpClient Client;
    private readonly TestServer _testServer;

    protected BaseClassFixture(TestFixture fixture)
    {
        Client = fixture.Client;
        _testServer = fixture.TestServer;
    }
}