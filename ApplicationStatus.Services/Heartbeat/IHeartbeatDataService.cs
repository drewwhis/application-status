namespace ApplicationStatus.Services.Heartbeat;

public interface IHeartbeatDataService
{
    Task<IEnumerable<Models.Heartbeat>> GetAll();
    Task<Models.Heartbeat> Create(Models.Heartbeat heartbeat);
}