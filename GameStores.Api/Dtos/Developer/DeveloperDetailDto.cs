namespace GameStores.Api.Dtos.Developer;

public record DeveloperDetailDto
(
    int Id,
    string StudioName,
    string Description,
    string Website,
    string Name,
    string Email
);
