using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Models;

[Index(nameof(ApplicationName), IsUnique = true)]
public record ApiUser
{
    [Key]
    public required Guid Id { get; init; }
    public required string ApplicationName { get; init; }
    public required string ApiKeyHash { get; init; }
}