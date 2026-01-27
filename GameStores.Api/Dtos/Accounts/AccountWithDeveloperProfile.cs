using GameStores.Api.Models;

namespace GameStores.Api.Dtos.Accounts;

public record AccountWithDeveloperProfile
(
    int Id,
    string Name,
    string Email,
    Role Role,

    string StudioName,
    string? Description,
    string? Website
);
