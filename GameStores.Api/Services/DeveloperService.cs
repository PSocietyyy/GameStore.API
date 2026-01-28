using GameStores.Api.Data;
using GameStores.Api.Dtos.Developer;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Services;

public class DeveloperService
{
    private readonly GameStoreContext _db;

    public DeveloperService(GameStoreContext db)
    {
        _db = db;
    }

    // READ ALL
    public async Task<List<DeveloperDto>> GetAllAsync()
    {
        return await _db.Developers
            .AsNoTracking()
            .Select(d => new DeveloperDto(
                d.Id,
                d.StudioName,
                d.Description,
                d.Website
            ))
            .ToListAsync();
    }

    // READ BY ID
    public async Task<DeveloperDetailDto?> GetByIdAsync(int id)
    {
        var developer = await _db.Developers
            .Include(d => d.Account)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (developer is null) return null;

        return new DeveloperDetailDto(
            developer.Id,
            developer.StudioName,
            developer.Description,
            developer.Website,
            developer.Account!.Name,
            developer.Account.Email
        );
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateDeveloperDto dto)
    {
        var developer = await _db.Developers.FindAsync(id);
        if (developer is null) return false;

        developer.StudioName = dto.StudioName;
        developer.Description = dto.Description;
        developer.Website = dto.Website;

        await _db.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _db.Developers
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
