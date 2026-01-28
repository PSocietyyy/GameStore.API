using GameStores.Api.Data;
using GameStores.Api.Dtos.Genre;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Services;

public class GenreService
{
    private readonly GameStoreContext _db;

    public GenreService(GameStoreContext db)
    {
        _db = db;
    }

    // CREATE
    public async Task<GenreDto> CreateAsync(CreateGenreDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        _db.Genres.Add(genre);
        await _db.SaveChangesAsync();

        return new GenreDto(
            genre.Id,
            genre.Name
        );
    }

    // READ ALL
    public async Task<List<GenreDto>> GetAllAsync()
    {
        return await _db.Genres
            .AsNoTracking()
            .Select(g => new GenreDto(
                g.Id,
                g.Name
            ))
            .ToListAsync();
    }

    // READ BY ID
    public async Task<GenreDto?> GetByIdAsync(int id)
    {
        var genre = await _db.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre is null) return null;

        return new GenreDto(
            genre.Id,
            genre.Name
        );
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateGenreDto dto)
    {
        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return false;

        genre.Name = dto.Name;
        await _db.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _db.Genres
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
