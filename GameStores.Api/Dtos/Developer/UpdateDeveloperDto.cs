using System.ComponentModel.DataAnnotations;

namespace GameStores.Api.Dtos.Developer;

public record UpdateDeveloperDto
(
    [Required][MaxLength(100)] string StudioName,
    [Required] string Description,
    [Required] string Website
);
