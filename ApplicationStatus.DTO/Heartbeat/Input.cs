namespace ApplicationStatus.DTO.Heartbeat;

public class Input
{
    public required bool IsGood { get; init; }
    public required string ApplicationName { get; init; }
}