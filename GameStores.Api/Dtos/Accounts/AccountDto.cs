using GameStores.Api.Models;

namespace GameStores.Api.Dtos.Accounts;

public record AccountDto(
    int Id,
    string Name,
    string Email,
    Role Role
);