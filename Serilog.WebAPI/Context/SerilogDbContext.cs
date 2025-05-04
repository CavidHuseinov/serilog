using Microsoft.EntityFrameworkCore;
using Serilog.WebAPI.Entities;
using System.Reflection;

namespace Serilog.WebAPI.Context
{
    public class SerilogDbContext : DbContext
    {
        public SerilogDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }
    }
}
