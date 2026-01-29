using System;
using GameStores.Api.Dtos.Developer;
using GameStores.Api.Dtos.Genre;

namespace GameStores.Api.Dtos.Game;

public record GameDetailDto(
    int Id,
    string Title,
    string Description,
    decimal Price,
    DateOnly ReleaseDate,
    string? ImageUrl,
    DeveloperDto Developer,
    List<GenreDto> Genres
);
