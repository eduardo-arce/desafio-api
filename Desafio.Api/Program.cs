using Desafio.Domain.Repository;
using Desafio.Domain.Service;
using Desafio.Infra.Context;
using Desafio.Infra.UnitOfWork;
using Desafio.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (connectionString == null)
    connectionString = builder.Configuration.GetConnectionString("Local");

builder.Services.AddDbContext<DesafioContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly(typeof(DesafioContext)
    .Assembly.FullName)));

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.AllowAnyHeader().AllowAnyOrigin().WithMethods("POST", "GET", "PUT", "DELETE"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
