using System.ComponentModel.DataAnnotations;

namespace GameStores.Api.Dtos.Accounts;

public class CreateAccountDto : IValidatableObject
{
    [Required]
    [MinLength(4)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Range(1, 3)]
    public int Role { get; set; }

    public string? StudioName { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Role == 2 && string.IsNullOrWhiteSpace(StudioName))
        {
            yield return new ValidationResult(
                "StudioName wajib diisi untuk role Developer",
                new[] { nameof(StudioName) }
            );
        }
    }
}
