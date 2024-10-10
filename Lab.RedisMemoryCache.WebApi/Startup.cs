using Lab.RedisMemoryCache.WebApi.Context;
using Lab.RedisMemoryCache.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab.RedisMemoryCache.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddHttpClient();
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddDbContext<ToDoListDbContext>(o => o.UseInMemoryDatabase("ToDoListDb"));
        services.AddScoped<ICachingService, CachingService>();

        services.AddStackExchangeRedisCache(o => {
            o.InstanceName = "instance";
            o.Configuration = "localhost:6379";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapControllers());
    }
}