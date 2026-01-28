using GameStores.Api.Dtos.Game;
using GameStores.Api.Services;

namespace GameStores.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET ALL
        group.MapGet("/", async (GameService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        // GET BY ID
        group.MapGet("/{id:int}", async (int id, GameService service) =>
        {
            var game = await service.GetByIdAsync(id);
            return game is null
                ? Results.NotFound()
                : Results.Ok(game);
        });

        // CREATE
        group.MapPost("/", async (CreateGameDto dto, GameService service) =>
        {
            var result = await service.CreateAsync(dto);
            return result.IsSuccess
                ? Results.Created($"/games/{result.GameId}", new { result.GameId })
                : Results.BadRequest(result.Error);
        });

        // UPDATE
        group.MapPut("/{id:int}", async (
            int id,
            UpdateGameDto dto,
            GameService service
        ) =>
        {
            var success = await service.UpdateAsync(id, dto);
            return success
                ? Results.NoContent()
                : Results.NotFound();
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, GameService service) =>
        {
            return await service.DeleteAsync(id)
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
