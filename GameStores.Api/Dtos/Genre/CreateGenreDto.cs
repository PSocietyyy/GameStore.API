using System.ComponentModel.DataAnnotations;

namespace GameStores.Api.Dtos.Genre;

public record CreateGenreDto
(
    [Required][StringLength(50)] string Name
);
