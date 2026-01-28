namespace GameStores.Api.Dtos.Game;

public record GameDto
(
    int Id,
    string Title,
    string Description,
    decimal Price,
    DateOnly ReleaseDate,
    string DeveloperName,
    List<string> Genres
);
