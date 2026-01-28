namespace GameStores.Api.Dtos.Game;

public record UpdateGameDto(
    string Title,
    string Description,
    decimal Price,
    DateOnly ReleaseDate,
    List<int> GenreIds
);