using Microsoft.AspNetCore.Identity;
using NewsApi.Middlewares;
using NewsApi.Models;
using NewsApi.Repositories;
using NewsApi.Services;
using Scalar.AspNetCore;
using NewsApi.Authentication;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;
using NewsApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose)
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Services
builder.Services.AddSqlServerDatabase(builder.Configuration);
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IArticleRepository, ArticlesRepository>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IFavouriteArticleRepository, FavouriteArticleRepository>();
builder.Services.AddScoped<IFavouriteArticlesService, FavouriteArticlesService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddOpenApi();

var app = builder.Build();

// Request pipleline
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
