using System;
using System.ComponentModel.DataAnnotations;

namespace GameStores.Api.Dtos.Accounts;

public record UpdateAccountDto(
    [Required][MinLength(4)][MaxLength(50)] string Name,
    [Required][EmailAddress] string Email,
    [MinLength(6)] string Password,
    [Required][Range(1,3)] int Role
);
