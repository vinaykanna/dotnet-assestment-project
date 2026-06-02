using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApi.Middlewares;
using NewsApi.Models;
using NewsApi.Repositories;
using NewsApi.Services;
using Scalar.AspNetCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NewsApi.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose)
//     .WriteTo.File(
//         "Logs/log-.txt",
//         rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });



builder.Services.AddAuthorization();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IArticleRepository, ArticlesRepository>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();


builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

