using ApplicationStatus.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Heartbeat> Heartbeats { get; set; }
}