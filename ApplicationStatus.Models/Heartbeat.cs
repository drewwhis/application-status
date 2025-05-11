using System.ComponentModel.DataAnnotations;

namespace ApplicationStatus.Models;

public record Heartbeat
{
    [Key]
    public required Guid Id { get; init; }
    public required bool IsGood { get; init; }
    public required DateTime LastUpdate { get; init; }
    public required string ApplicationName { get; init; }
}