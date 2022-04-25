using CurerencyApi.Infrastructure.Providers;
using CurrencyApi.Core.Interfaces;
using CurrencyApi.Core.Options;
using CurrencyApi.Core.Services;
using CurrencyApi.Web.Options;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var corsOptions = builder.Configuration
        .GetSection("CorsOptions")
        .Get<CorsOptions>();

    builder.Host.UseSerilog((ctx, config) =>
    {
        config.ReadFrom.Configuration(ctx.Configuration);
    });

    builder.Services.Configure<CurrencyClientOptions>(builder.Configuration.GetSection("CurrencyClientOptions"));

    builder.Services.AddCors();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ICurrencyClient, CbrCurrencyClient>();
    builder.Services.AddScoped<ICurrencyService, CurrencyService>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    }
    else
    {
        app.UseCors(policy =>
        {
            policy
                .WithOrigins(corsOptions.AllowedOrigins)
                .WithHeaders(corsOptions.AllowedHeaders)
                .WithMethods(corsOptions.AllowedMethods);
        });
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, @"Unhandled Exception {ExceptionType}", ex.GetType().Name);
}
finally{
    Log.Information("Shutdown is complete");
    Log.CloseAndFlush();
}