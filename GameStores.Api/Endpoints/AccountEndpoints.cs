using System;
using System.Security.Cryptography.X509Certificates;
using GameStores.Api.Data;
using GameStores.Api.Dtos.Accounts;
using GameStores.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        // GetAll Accounts /
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var accounts = await dbContext.Accounts
                .Select(a => new AccountDto(
                    a.Id,
                    a.Name,
                    a.Email,
                    a.Role
                )).AsNoTracking().ToListAsync();
            return Results.Ok(accounts);
        });

        // GetById /{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var account = await dbContext.Accounts.Include(a => a.DeveloperProfile).FirstOrDefaultAsync(a => a.Id == id);
            if(account is null)
            {
                return Results.NotFound();
          }

          if(account.Role == Role.Developer)
            {
                return Results.Ok(new AccountWithDeveloperProfile(
                    account.Id,
                    account.Name,
                    account.Email,
                    account.Role,
                    account.DeveloperProfile!.StudioName,
                    account.DeveloperProfile.Description,
                    account.DeveloperProfile.Website
                ));
            }
            return Results.Ok(new AccountDto(
                account.Id,
                account.Name,
                account.Email,
                account.Role
            ));
        }).WithName("GetAccountById");

        // Create
        group.MapPost("/", async (CreateAccountDto newAccount, GameStoreContext dbContext) =>
        {
            var passwordHash = new PasswordHasher<Account>();
            Account account = new()
            {
                Name = newAccount.Name,
                Email = newAccount.Email,
                Role = (Role)newAccount.Role
            };
            account.Password = passwordHash.HashPassword(account, newAccount.Password);
            dbContext.Add(account);
            await dbContext.SaveChangesAsync();
            // Jika Akun developer
            if((Role)newAccount.Role == Role.Developer)
            {
                // Buat Profile
                Developer developer = new()
                {
                    StudioName = newAccount.StudioName!,
                    AccountId = account.Id
                };
                dbContext.Add(developer);
                await dbContext.SaveChangesAsync();
            }
            return Results.CreatedAtRoute("GetAccountById", new {id = account.Id},new AccountDto(
                Id: account.Id,
                Name: account.Name,
                Email: account.Email,
                Role: account.Role
            ));
        });

        // Edit Data
        group.MapPut("/{id}", async (int id, UpdateAccountDto updatedAccount, GameStoreContext dbContext) =>
        {
            // Cari akun berdasarkan id
            var existingAccount = await dbContext.Accounts.FindAsync(id);
            // Cek apakah akun tersedia
            if(existingAccount is null)
            {
                return Results.NotFound();
            }

            // Update data
            existingAccount.Name = updatedAccount.Name;
            existingAccount.Email = updatedAccount.Email;
            // apakah user mengedit password
            if(!string.IsNullOrWhiteSpace(updatedAccount.Password))
            {
                var passowrdHash = new PasswordHasher<Account>();
                existingAccount.Password = passowrdHash.HashPassword(existingAccount, updatedAccount.Password);
            }
            existingAccount.Role = (Role)updatedAccount.Role;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // Delete
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var existingAccount = await dbContext.Accounts.FindAsync(id);
            if(existingAccount is null)
            {
                return Results.NotFound();
            }

            if(existingAccount.Role == Role.Developer)
            {
                await dbContext.Developers.Where(d => d.AccountId == id).ExecuteDeleteAsync();
            }
            await dbContext.Accounts.Where(a => a.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
