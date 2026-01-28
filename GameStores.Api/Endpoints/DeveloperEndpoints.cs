using GameStores.Api.Dtos.Developer;
using GameStores.Api.Services;

namespace GameStores.Api.Endpoints;

public static class DeveloperEndpoints
{
    public static void MapDeveloperEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/developers");

        // GET ALL
        group.MapGet("/", async (DeveloperService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        // GET BY ID
        group.MapGet("/{id:int}", async (int id, DeveloperService service) =>
        {
            var developer = await service.GetByIdAsync(id);
            return developer is null
                ? Results.NotFound()
                : Results.Ok(developer);
        });

        // UPDATE
        group.MapPut("/{id:int}", async (
            int id,
            UpdateDeveloperDto dto,
            DeveloperService service
        ) =>
        {
            return await service.UpdateAsync(id, dto)
                ? Results.NoContent()
                : Results.NotFound();
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, DeveloperService service) =>
        {
            return await service.DeleteAsync(id)
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
