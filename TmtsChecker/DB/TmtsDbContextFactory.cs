using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TmtsChecker.DB
{    
    public class TmtsDbContextFactory : IDesignTimeDbContextFactory<TmtsDbContext>
    {
        public TmtsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TmtsDbContext>();            
            optionsBuilder.UseSqlite("Data Source = ./TmtsDB.db");
            return new TmtsDbContext(optionsBuilder.Options);
        }
    }
}
