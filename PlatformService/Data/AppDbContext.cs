using Microsoft.EntityFrameworkCore;
using PlatformService.Entities;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
            
    }

    public DbSet<Platform> Platforms { get; set; }
}