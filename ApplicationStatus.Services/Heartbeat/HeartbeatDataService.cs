using ApplicationStatus.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Services.Heartbeat;

public class HeartbeatDataService(AppDbContext db) : IHeartbeatDataService
{
    public async Task<IEnumerable<Models.Heartbeat>> GetAll()
    {
        return await db.Heartbeats.ToListAsync();
    }

    public Task<Models.Heartbeat> Create(Models.Heartbeat heartbeat)
    {
        throw new NotImplementedException();
    }
}