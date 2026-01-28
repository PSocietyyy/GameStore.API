using GameStores.Api.Data;
using GameStores.Api.Dtos.Accounts;
using GameStores.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Services;

public class AccountService
{
    private readonly GameStoreContext _db;
    private readonly PasswordHasher<Account> _hasher = new();

    public AccountService(GameStoreContext db)
    {
        _db = db;
    }

    // CREATE
    public async Task<Account> CreateAsync(CreateAccountDto dto)
    {
        var account = new Account
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = (Role)dto.Role
        };

        account.Password = _hasher.HashPassword(account, dto.Password);
        _db.Accounts.Add(account);

        if (account.Role == Role.Developer)
        {
            var developer = new Developer
            {
                StudioName = dto.StudioName!,
                Account = account // 🔑 biar FK keisi otomatis
            };
            _db.Developers.Add(developer);
        }

        await _db.SaveChangesAsync();
        return account;
    }

    // READ ALL
    public async Task<List<AccountDto>> GetAllAsync()
    {
        return await _db.Accounts
            .AsNoTracking()
            .Select(a => new AccountDto(
                a.Id,
                a.Name,
                a.Email,
                a.Role
            ))
            .ToListAsync();
    }

    // READ BY ID
    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _db.Accounts
            .Include(a => a.DeveloperProfile)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateAccountDto dto)
    {
        var account = await _db.Accounts.FindAsync(id);
        if (account is null) return false;

        account.Name = dto.Name;
        account.Email = dto.Email;
        account.Role = (Role)dto.Role;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            account.Password = _hasher.HashPassword(account, dto.Password);
        }

        await _db.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var account = await _db.Accounts.FindAsync(id);
        if (account is null) return false;

        if (account.Role == Role.Developer)
        {
            await _db.Developers
                .Where(d => d.AccountId == id)
                .ExecuteDeleteAsync();
        }

        await _db.Accounts
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return true;
    }
}
