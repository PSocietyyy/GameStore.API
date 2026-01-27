using System;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<GameGenre> GameGenres => Set<GameGenre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure GameGenre composite key
        modelBuilder.Entity<GameGenre>()
            .HasKey(gg => new { gg.GameId, gg.GenreId });

        // Configure Developer → Games 1-to-many
        modelBuilder.Entity<Developer>()
            .HasMany(d => d.Games)
            .WithOne(g => g.Developer)
            .HasForeignKey(g => g.DeveloperId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure GameGenre relationships
        modelBuilder.Entity<GameGenre>()
            .HasOne(gg => gg.Game)
            .WithMany(g => g.GameGenres)
            .HasForeignKey(gg => gg.GameId);

        modelBuilder.Entity<GameGenre>()
            .HasOne(gg => gg.Genre)
            .WithMany(g => g.GameGenres)
            .HasForeignKey(gg => gg.GenreId);
    }

}
