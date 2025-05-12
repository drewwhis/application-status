using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Models;

[Index(nameof(ApplicationName), IsUnique = true)]
[Index(nameof(Prefix), nameof(ApiKeyHash), IsUnique = true)]
public record ApiUser
{
    [Key]
    public required Guid Id { get; init; }
    
    public required string ApplicationName { get; init; }
    
    public required string ApiKeyHash { get; init; }
    
    [StringLength(7, MinimumLength = 7)]
    public required string Prefix { get; init; }
}