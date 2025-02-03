using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities;

public class User : BaseEntity
{
    [Required] [MaxLength(64)] public required string Username { get; set; }

    [MaxLength(128)] public string? FullName { get; set; }

    [Required] [MaxLength(320)] public required string Email { get; set; }

    [Required] [MaxLength(128)] public required string Password { get; set; }
}