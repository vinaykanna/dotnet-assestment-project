using Microsoft.EntityFrameworkCore;
using NewsApi.DTOs;
using NewsApi.Models;

namespace NewsApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<FavouriteArticle> FavouriteArticles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleView> ArticleViews { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TrendingArticleDto>()
            .HasNoKey()
            .ToView(null);

        modelBuilder.Entity<Article>()
               .HasOne(a => a.Author)
               .WithMany(u => u.AuthoredArticles)
               .HasForeignKey(a => a.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FavouriteArticle>()
            .HasOne(f => f.User)
            .WithMany(u => u.FavouritedArticles)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FavouriteArticle>()
            .HasOne(f => f.Article)
            .WithMany()
            .HasForeignKey(f => f.ArticleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}