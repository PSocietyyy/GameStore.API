namespace GameStores.Api.Dtos.Game;

public record CreateGameDto(
    string Title,
    string Description,
    decimal Price,
    DateOnly ReleaseDate,
    int DeveloperId,
    List<int> GenreIds
);
