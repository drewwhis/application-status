using ApplicationStatus.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Services.Heartbeat;

public class HeartbeatService(AppDbContext db) : IHeartbeatService
{
    public async Task<IEnumerable<Models.Heartbeat>> GetAll()
    {
        return await db.Heartbeats.ToListAsync();
    }
}