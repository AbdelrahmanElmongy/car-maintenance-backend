using Microsoft.EntityFrameworkCore;
using CarMaintenance.Models;

namespace CarMaintenance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TestItem> TestItems { get; set; }
    }
}