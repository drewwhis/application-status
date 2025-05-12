using ApplicationStatus.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<ApiUser> ApiUsers { get; init; }
    public DbSet<Heartbeat> Heartbeats { get; set; }
}