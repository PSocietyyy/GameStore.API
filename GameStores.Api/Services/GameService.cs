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
    private readonly IWebHostEnvironment _env;

    public GameService(GameStoreContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
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
                g.GameGenres.Select(gg => gg.Genre.Name).ToList(),
                g.ImageUrl
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
                g.ImageUrl,
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

        string? imagePath = null;
        if(dto.Image is not null)
        {
            var webRoot = _env.WebRootPath ?? throw new InvalidOperationException("Web root belum di konfigurasi");
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await dto.Image.CopyToAsync(stream);
            imagePath = $"/images/{fileName}";
        }

        var game = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate,
            DeveloperId = dto.DeveloperId,
            ImageUrl = imagePath,
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

    // Update Foto
    public async Task<bool> UpdateImageAsync(int id, IFormFile image)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return false;

        var webRoot = _env.WebRootPath
            ?? throw new InvalidOperationException("WebRootPath belum dikonfigurasi");

        var uploadsFolder = Path.Combine(webRoot, "games");
        Directory.CreateDirectory(uploadsFolder);

        // hapus lama
        if (!string.IsNullOrWhiteSpace(game.ImageUrl))
        {
            var oldPath = Path.Combine(webRoot, game.ImageUrl.TrimStart('/'));
            if (File.Exists(oldPath))
                File.Delete(oldPath);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var fullPath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await image.CopyToAsync(stream);

        game.ImageUrl = $"/games/{fileName}";
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
