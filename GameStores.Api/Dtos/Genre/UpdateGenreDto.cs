using System.ComponentModel.DataAnnotations;

namespace GameStores.Api.Dtos.Genre;

public record  UpdateGenreDto
(
    [Required][StringLength(50)] string Name
);