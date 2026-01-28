using System;
using GameStores.Api.Data;
using GameStores.Api.Dtos.Genre;
using GameStores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // Get All genre
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var genres = await dbContext.Genres.Select(g => new GenreDto(
                Id: g.Id,
                Name: g.Name
            )).AsNoTracking().ToListAsync();
            return Results.Ok(genres);
        });

        // Get Genre By Id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var genre = await dbContext.Genres.FindAsync(id);
            if(genre is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(new GenreDto(Id: genre.Id, Name: genre.Name));
        }).WithName("GetGenreById");

        // Create Genre
        group.MapPost("/", async (CreateGenreDto newGenre, GameStoreContext dbContext) =>
        {
            Genre genre = new()
            {
                Name = newGenre.Name
            };

            dbContext.Add(genre);
            await dbContext.SaveChangesAsync();
            var createdGenre = new GenreDto(Id: genre.Id, Name: genre.Name);
            return Results.CreatedAtRoute("GetGenreById", new {id = genre.Id}, createdGenre);
        });

        // UpdateGenre
        group.MapPut("/{id}", async(int id, UpdateGenreDto updatedGenre, GameStoreContext dbContext) =>
        {
            var existingGenre = await dbContext.Genres.FindAsync(id);
            if(existingGenre is null)
            {
                return Results.NotFound();
            }
            existingGenre.Name = updatedGenre.Name;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // Delete Genre
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Genres.Where(g=>g.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
