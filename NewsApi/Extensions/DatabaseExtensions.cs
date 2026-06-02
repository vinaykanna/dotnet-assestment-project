using Microsoft.EntityFrameworkCore;
using NewsApi.Data;

namespace NewsApi.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddSqlServerDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}