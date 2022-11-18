using HashWorkerBlazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MudBlazor.Charts;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HashWorkerBlazor.DB
{
    //dotnet ef migrations add initDB
    //dotnet ef database update

    public class HashWorkerDbContext : DbContext
    {
        public HashWorkerDbContext(DbContextOptions<HashWorkerDbContext> options) : base(options)
        {
        }
        
        public DbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create seed data
            //modelBuilder.Entity<ListItem>().HasData(
            //        new { Id=1, Account = "Test", CreateTime = DateTime.Now, FolderPath = "Test", HashListJson = "Test", CheckHash = "Test" }
            //);
        }
    }

    //public class HashWorkerDbContextFactory : IDesignTimeDbContextFactory<HashWorkerDbContext>
    //{
    //    public HashWorkerDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<HashWorkerDbContext>();
    //        optionsBuilder.UseSqlite("Data Source = ./TmtsDB.db"); //nuget install Microsoft.EntityFrameworkCore.Sqlite
    //        return new HashWorkerDbContext(optionsBuilder.Options);
    //    }
    //}
}
