using System;
using System.Net.Http;
using Api.Configuration.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Api.IntegrationTests.Base;

public class TestFixture : IDisposable
{
    public readonly TestServer TestServer;

    public HttpClient Client { get; }
        
    public TestFixture()
    {
        var builder = new WebHostBuilder()
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);

                var env = hostContext.HostingEnvironment;

                //configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                configApp.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true,
                    reloadOnChange: true);
            })
            .UseStartup<StartupTest>();


        TestServer = new TestServer(builder);
        Client = TestServer.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        TestServer.Dispose();
    }
}