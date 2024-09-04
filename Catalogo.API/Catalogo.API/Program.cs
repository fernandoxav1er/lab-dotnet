using Catalogo.API.Context;
using Catalogo.API.Extensions;
using Catalogo.API.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

/* Database */
var conn = builder.Configuration.GetConnectionString("ConnectionMySql");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

/* Services */
builder.Services.AddControllers(options =>{options.Filters.Add(typeof(ApiExceptionFilter));})
                .AddJsonOptions(options =>{options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Log por filtros na controller, estoura na console */
//builder.Services.AddScoped<ApiLoggingFilter>();

/* Log em txt externo da api */
//builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration{LogLevel = LogLevel.Debug}));

var app = builder.Build();

/* Aplicando migrations e seed que esta nas migrations */
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro durante a migracao ou a propagacao da base de dados.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();