using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Machine> Machines => Set<Machine>();
        public DbSet<Component> Components => Set<Component>();
    }
}
