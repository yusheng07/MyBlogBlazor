using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TmtsChecker.Models;

namespace TmtsChecker.DB
{
    public class TmtsDbContext : DbContext
    {
        public TmtsDbContext(DbContextOptions<TmtsDbContext> options) : base(options)
        {
        }

        public DbSet<TmHost> Hosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create seed data
            modelBuilder.Entity<TmHost>().HasData(
                    new { Hostname = "PC01", Ip = String.Empty },
                    new { Hostname = "PC02", Ip = String.Empty },
                    new { Hostname = "PC03", Ip = String.Empty }
            );
        }
    }
}