using NewsApi.Services.Interfaces;

namespace NewsApi.Services;

public class TrendingBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<TrendingBackgroundService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background cache service for trending articles initialized.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Running Background cache service for trending articles at: {time}",
                DateTimeOffset.Now);

            using var scope = scopeFactory.CreateScope();

            var trendingService = scope.ServiceProvider
                    .GetRequiredService<ITrendingArticlesService>();

            await trendingService.RefreshTrendingArticles();

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}