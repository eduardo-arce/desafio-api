using Desafio.Domain.Repository;
using Desafio.Domain.Service;
using Desafio.Infra.Context;
using Desafio.Infra.UnitOfWork;
using Desafio.Service;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Desafio.Infra.Repository;
using Microsoft.OpenApi.Models;

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserProfile>();
});

IMapper mapper = config.CreateMapper();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API com JWT", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
    });
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (connectionString == null)
    connectionString = builder.Configuration.GetConnectionString("Local");

builder.Services.AddDbContext<DesafioContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly(typeof(DesafioContext)
    .Assembly.FullName)));

builder.Services.AddSignalR();

builder.Services.AddHttpContextAccessor();

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

app.UseCors();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<UpdateNotificationService>("/Notification");
});

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
