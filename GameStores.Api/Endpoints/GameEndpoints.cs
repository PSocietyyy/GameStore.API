using System;
using GameStores.Api.Data;
using GameStores.Api.Dtos.Developer;
using GameStores.Api.Dtos.Game;
using GameStores.Api.Dtos.Genre;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // Get ALL Games
        group.MapGet("/", async (GameStoreContext db) =>
        {
            var games = await db.Games
                .Include(g => g.Developer)
                .Include(g => g.GameGenres)
                    .ThenInclude(gg => gg.Genre)
                .Select(g => new GameDto(
                    g.Id,
                    g.Title,
                    g.Description,
                    g.Price,
                    g.ReleaseDate,
                    g.Developer!.StudioName,
                    g.GameGenres
                        .Select(gg => gg.Genre.Name)
                        .ToList()
                ))
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(games);
        });

        // Get Game By Id
        group.MapGet("/{id}", async (int id, GameStoreContext db) =>
        {
            var game = await db.Games
                .Include(g => g.Developer)
                .Include(g => g.GameGenres)
                    .ThenInclude(gg => gg.Genre)
                .Where(g => g.Id == id)
                .Select(g => new GameDetailDto(
                    g.Id,
                    g.Title,
                    g.Description,
                    g.Price,
                    g.ReleaseDate,
                    new DeveloperDto(
                        g.Developer!.Id,
                        g.Developer.StudioName,
                        g.Developer.Description,
                        g.Developer.Website
                    ),
                    g.GameGenres
                        .Select(gg => new GenreDto(
                            gg.Genre.Id,
                            gg.Genre.Name
                        ))
                        .ToList()
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return game is null
                ? Results.NotFound()
                : Results.Ok(game);
        });

        // Create Game DTO
        group.MapPost("/", async (CreateGameDto dto, GameStoreContext db) =>
        {
            // validasi developer
            var developerExists = await db.Developers
                .AnyAsync(d => d.Id == dto.DeveloperId);

            if (!developerExists)
                return Results.BadRequest("Developer tidak ditemukan");

            // validasi genre
            var genres = await db.Genres
                .Where(g => dto.GenreIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != dto.GenreIds.Count)
                return Results.BadRequest("Salah satu genre tidak ditemukan");

            var game = new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate,
                DeveloperId = dto.DeveloperId
            };

            // mapping many-to-many
            game.GameGenres = genres
                .Select(g => new GameGenre
                {
                    GenreId = g.Id
                })
                .ToList();

            db.Games.Add(game);
            await db.SaveChangesAsync();

            return Results.Created($"/games/{game.Id}", new { game.Id });
        });

        // Update Game
        group.MapPut("/{id}", async (int id, UpdateGameDto dto, GameStoreContext db) =>
        {
            var game = await db.Games
                .Include(g => g.GameGenres)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game is null)
                return Results.NotFound();

            game.Title = dto.Title;
            game.Description = dto.Description;
            game.Price = dto.Price;
            game.ReleaseDate = dto.ReleaseDate;

            // update genres
            db.GameGenres.RemoveRange(game.GameGenres);

            game.GameGenres = dto.GenreIds
                .Select(genreId => new GameGenre
                {
                    GameId = id,
                    GenreId = genreId
                })
                .ToList();

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        // Delete Game
        group.MapDelete("/{id}", async (int id, GameStoreContext db) =>
        {
            var game = await db.Games.FindAsync(id);

            if (game is null)
                return Results.NotFound();

            await db.Games
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

    }
}
