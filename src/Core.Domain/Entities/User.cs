using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    
    [Required] 
    [MaxLength(64)]
    public required string Username { get; set; }
    
    [MaxLength(128)]
    public string? FullName { get; set; }
    
    [Required] 
    [MaxLength(320)]
    public required string Email { get; set; }
    
    [Required] 
    [MaxLength(128)]
    public required string Password { get; set; }
    
    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModifiedDate { get; set; }
}