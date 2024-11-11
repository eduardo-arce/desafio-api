using Desafio.Domain.Repository;
using Desafio.Domain.Service;
using Desafio.Infra.Context;
using Desafio.Infra.UnitOfWork;
using Desafio.Service;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Desafio.Infra.Repository;

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserProfile>();
});

IMapper mapper = config.CreateMapper();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (connectionString == null)
    connectionString = builder.Configuration.GetConnectionString("Local");

builder.Services.AddDbContext<DesafioContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly(typeof(DesafioContext)
    .Assembly.FullName)));

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IUpdateNotificationService, UpdateNotificationService>();

builder.Services.AddTransient<ILoginService, LoginService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.AllowAnyHeader().AllowAnyOrigin().WithMethods("POST", "GET", "PUT", "DELETE"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<UpdateNotificationService>("/UpdateNotification");
});

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
