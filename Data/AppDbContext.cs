using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Lubricante> Lubricantes => Set<Lubricante>();

    }
}
