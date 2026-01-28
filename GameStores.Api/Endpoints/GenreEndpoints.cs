using GameStores.Api.Dtos.Genre;
using GameStores.Api.Services;

namespace GameStores.Api.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET ALL
        group.MapGet("/", async (GenreService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        // GET BY ID
        group.MapGet("/{id:int}", async (int id, GenreService service) =>
        {
            var genre = await service.GetByIdAsync(id);
            return genre is null
                ? Results.NotFound()
                : Results.Ok(genre);
        })
        .WithName("GetGenreById");

        // CREATE
        group.MapPost("/", async (CreateGenreDto dto, GenreService service) =>
        {
            var genre = await service.CreateAsync(dto);
            return Results.CreatedAtRoute(
                "GetGenreById",
                new { id = genre.Id },
                genre
            );
        });

        // UPDATE
        group.MapPut("/{id:int}", async (
            int id,
            UpdateGenreDto dto,
            GenreService service
        ) =>
        {
            return await service.UpdateAsync(id, dto)
                ? Results.NoContent()
                : Results.NotFound();
        });

        // DELETE
        group.MapDelete("/{id:int}", async (int id, GenreService service) =>
        {
            return await service.DeleteAsync(id)
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
