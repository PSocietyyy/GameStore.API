using System;
using GameStores.Api.Data;
using GameStores.Api.Dtos.Developer;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Endpoints;

public static class DeveloperEndpoints
{
    public static void MapDeveloperEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/developers");
        // GetAllDevelopers
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var developers = await dbContext.Developers.Select(d => new DeveloperDto(
                Id: d.Id,
                StudioName: d.StudioName,
                Description: d.Description,
                Website: d.Website
            )).AsNoTracking().ToListAsync();
            return Results.Ok(developers);
        });

        // GetDetailDevelopers
        group.MapGet("/{id}", async (int id, GameStoreContext dbContexts) =>
        {
            var existingDeveloperProfile = await dbContexts.Developers.Include(d => d.Account).Where(d => d.Id == id).FirstOrDefaultAsync();
            if(existingDeveloperProfile is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new DeveloperDetailDto(
                Id: existingDeveloperProfile.Id,
                StudioName: existingDeveloperProfile.StudioName,
                Description: existingDeveloperProfile.Description,
                Website: existingDeveloperProfile.Website,
                Name: existingDeveloperProfile.Account!.Name,
                Email: existingDeveloperProfile.Account.Email
            ));
        });

        // Update Developer
        group.MapPut("/{id}", async(int id, UpdateDeveloperDto updatedDeveloper, GameStoreContext dbContext) =>
        {
            var existingDeveloper = await dbContext.Developers.FindAsync(id);
            if(existingDeveloper is null)
            {
                return Results.NotFound();
            }
            existingDeveloper.StudioName = updatedDeveloper.StudioName;
            existingDeveloper.Description = updatedDeveloper.Description;
            existingDeveloper.Website = updatedDeveloper.Website;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
