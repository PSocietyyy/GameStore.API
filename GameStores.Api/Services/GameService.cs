using GameStores.Api.Data;
using GameStores.Api.Dtos.Developer;
using GameStores.Api.Dtos.Game;
using GameStores.Api.Dtos.Genre;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Services;

public class GameService
{
    private readonly GameStoreContext _db;

    public GameService(GameStoreContext db)
    {
        _db = db;
    }

    // READ ALL
    public async Task<List<GameDto>> GetAllAsync()
    {
        return await _db.Games
            .Include(g => g.Developer)
            .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
            .AsNoTracking()
            .Select(g => new GameDto(
                g.Id,
                g.Title,
                g.Description,
                g.Price,
                g.ReleaseDate,
                g.Developer!.StudioName,
                g.GameGenres.Select(gg => gg.Genre.Name).ToList()
            ))
            .ToListAsync();
    }

    // READ BY ID
    public async Task<GameDetailDto?> GetByIdAsync(int id)
    {
        return await _db.Games
            .Include(g => g.Developer)
            .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
            .AsNoTracking()
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
                g.GameGenres.Select(gg =>
                    new GenreDto(gg.Genre.Id, gg.Genre.Name)
                ).ToList()
            ))
            .FirstOrDefaultAsync();
    }

    // CREATE
    public async Task<(bool IsSuccess, int GameId, string? Error)> CreateAsync(CreateGameDto dto)
    {
        if (!await _db.Developers.AnyAsync(d => d.Id == dto.DeveloperId))
            return (false, 0, "Developer tidak ditemukan");

        var genres = await _db.Genres
            .Where(g => dto.GenreIds.Contains(g.Id))
            .ToListAsync();

        if (genres.Count != dto.GenreIds.Count)
            return (false, 0, "Salah satu genre tidak ditemukan");

        var game = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate,
            DeveloperId = dto.DeveloperId,
            GameGenres = genres.Select(g => new GameGenre
            {
                GenreId = g.Id
            }).ToList()
        };

        _db.Games.Add(game);
        await _db.SaveChangesAsync();

        return (true, game.Id, null);
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateGameDto dto)
    {
        var game = await _db.Games
            .Include(g => g.GameGenres)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game is null) return false;

        game.Title = dto.Title;
        game.Description = dto.Description;
        game.Price = dto.Price;
        game.ReleaseDate = dto.ReleaseDate;

        _db.GameGenres.RemoveRange(game.GameGenres);

        game.GameGenres = dto.GenreIds.Select(genreId =>
            new GameGenre
            {
                GameId = id,
                GenreId = genreId
            }
        ).ToList();

        await _db.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _db.Games
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
