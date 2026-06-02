
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NewsApi.DTOs;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;

namespace NewsApi.Services;

public class TrendingArticlesService(
    IDistributedCache cache,
    IArticleRepository articleRepository,
    ILogger<TrendingArticlesService> logger
    ) : ITrendingArticlesService
{
    private const string CacheKey = "trending_articles";

    public async Task<List<TrendingArticleDto>> GetTrendingArticles()
    {
        var cachedData = await cache.GetStringAsync(CacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            logger.LogInformation("Returning cached trending data");
            return JsonSerializer.Deserialize<List<TrendingArticleDto>>(cachedData)!;
        }

        await RefreshTrendingArticles();

        cachedData = await cache.GetStringAsync(CacheKey);

        return JsonSerializer.Deserialize<List<TrendingArticleDto>>(cachedData!)!;
    }

    public async Task RefreshTrendingArticles()
    {
        var trending = await articleRepository.GetTrendingArticlesAsync();

        var json = JsonSerializer.Serialize(trending);

        logger.LogInformation("Refreshing trending data");

        await cache.SetStringAsync(CacheKey, json, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }
}