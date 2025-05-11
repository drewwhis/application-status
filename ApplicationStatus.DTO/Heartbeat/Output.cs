namespace ApplicationStatus.DTO.Heartbeat;

public class Output
{
    public required bool IsGood { get; init; }
    public required DateTime LastUpdate { get; init; }
    public required string ApplicationName { get; init; }
}