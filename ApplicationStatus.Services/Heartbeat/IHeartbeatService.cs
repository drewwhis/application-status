namespace ApplicationStatus.Services.Heartbeat;

public interface IHeartbeatService
{
    Task<IEnumerable<Models.Heartbeat>> GetAll();
}