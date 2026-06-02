using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<User> Favourites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<FavoriteArticle>()
            .HasOne(fa => fa.User)
            .WithMany(u => u.FavoritedArticles)
            .HasForeignKey(fa => fa.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FavoriteArticle>()
            .HasOne(fa => fa.Article)
            .WithMany(a => a.FavoritedBy)
            .HasForeignKey(fa => fa.ArticleId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}