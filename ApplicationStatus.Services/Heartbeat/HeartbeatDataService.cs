using ApplicationStatus.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Services.Heartbeat;

public class HeartbeatDataService(AppDbContext db) : IHeartbeatDataService
{
    public async Task<IEnumerable<Models.Heartbeat>> GetAll()
    {
        return await db.Heartbeats.AsNoTracking().ToListAsync();
    }

    public async Task<Models.Heartbeat?> Get(string applicationName)
    {
        return await db.Heartbeats.AsNoTracking().FirstOrDefaultAsync(h => h.ApplicationName == applicationName);
    }

    public async Task<Models.Heartbeat> Create(Models.Heartbeat heartbeat)
    {
        await db.Heartbeats.AddAsync(heartbeat);
        await db.SaveChangesAsync();
        await db.Heartbeats.Entry(heartbeat).ReloadAsync();
        return heartbeat;
    }

    public async Task<Models.Heartbeat> Update(Models.Heartbeat heartbeat)
    {
        db.Heartbeats.Update(heartbeat);
        await db.SaveChangesAsync();
        await db.Heartbeats.Entry(heartbeat).ReloadAsync();
        return heartbeat;
    }
}