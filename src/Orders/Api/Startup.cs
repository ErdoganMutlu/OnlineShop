using Api.Configuration.AppSettings;
using Api.Managers;
using Api.ObjectModels;
using Api.RabbitMqServiceClients;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = new AppSettings(Configuration);
        services.AddSingleton(appSettings);
        services.AddHttpContextAccessor();
        services.AddHttpClient();

        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
            
        services.AddControllers();
        services.InitializeManagers<AppSettings>(Configuration);
        services.InitializeObjectModels(Configuration);
        services.InitializeRabbitMqServiceClients<AppSettings>();
            
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(appSettings.Api_Version,
                new OpenApiInfo { Title = appSettings.Api_Name, Version = appSettings.Api_Version });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings appSettings)
    {
        if (env.IsDevelopment() || env.IsStaging() || env.IsProduction())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{appSettings.Api_BaseUrl}/swagger/v1/swagger.json",
                    appSettings.Api_Name);
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}