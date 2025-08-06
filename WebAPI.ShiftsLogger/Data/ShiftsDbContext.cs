using Microsoft.EntityFrameworkCore;

namespace WebAPI.ShiftsLogger.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Shift> Shifts { get; set; }
}
