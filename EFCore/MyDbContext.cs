using Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }

    DbSet<Employee> Employees { get; set; } = default!;
    DbSet<TimeEntry> TimeEntries { get; set; } = default!;
}
