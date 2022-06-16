using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Api.Configuration.Test;

public class StartupTest : Startup
{
    public StartupTest(IWebHostEnvironment env, IConfiguration configuration) : base(configuration)
    {
    }
}