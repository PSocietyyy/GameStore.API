using GameStores.Api.Dtos.Accounts;
using GameStores.Api.Models;
using GameStores.Api.Services;

namespace GameStores.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        // GET ALL
        group.MapGet("/", async (AccountService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        // GET BY ID
        group.MapGet("/{id:int}", async (int id, AccountService service) =>
        {
            var account = await service.GetByIdAsync(id);
            if (account is null)
                return Results.NotFound();

            if (account.Role == Role.Developer && account.DeveloperProfile is not null)
            {
                return Results.Ok(new AccountWithDeveloperProfile(
                    account.Id,
                    account.Name,
                    account.Email,
                    account.Role,
                    account.DeveloperProfile.StudioName,
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
        })
        .WithName("GetAccountById");

        // CREATE
        group.MapPost("/", async (CreateAccountDto dto, AccountService service) =>
        {
            var account = await service.CreateAsync(dto);

            return Results.CreatedAtRoute(
                "GetAccountById",
                new { id = account.Id },
                new AccountDto(
                    account.Id,
                    account.Name,
                    account.Email,
                    account.Role
                )
            );
        });

        // UPDATE
        group.MapPut("/{id:int}", async (
            int id,
            UpdateAccountDto dto,
            AccountService service
        ) =>
        {
            return await service.UpdateAsync(id, dto)
                ? Results.NoContent()
                : Results.NotFound();
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, AccountService service) =>
        {
            return await service.DeleteAsync(id)
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
