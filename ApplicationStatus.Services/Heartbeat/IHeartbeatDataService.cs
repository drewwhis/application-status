namespace ApplicationStatus.Services.Heartbeat;

public interface IHeartbeatDataService
{
    Task<IEnumerable<Models.Heartbeat>> GetAll();
    Task<Models.Heartbeat?> Get(string applicationName);
    Task<Models.Heartbeat> Create(Models.Heartbeat heartbeat);
    Task<Models.Heartbeat> Update(Models.Heartbeat heartbeat);
}