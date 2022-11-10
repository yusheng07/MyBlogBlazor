using Microsoft.EntityFrameworkCore;
using TmtsChecker.Models;

namespace TmtsChecker.DB
{
    public class TmtsDataApi : ITmtsDataApi
    {
        private IDbContextFactory<TmtsDbContext> factory;

        public TmtsDataApi(IDbContextFactory<TmtsDbContext> factory)
        {
            this.factory = factory;
        }

        public async Task<List<TmHost>> GetHostsAsync()
        {
            using var context = factory.CreateDbContext();
            return await context.Hosts.ToListAsync();
        }

        public async Task ReplaceHostsAsync(List<string> hostnames)
        {
            using var context = factory.CreateDbContext();
            //remove all elements
            context.Database.ExecuteSqlRaw("delete from [Hosts]");
            //insert rows
            context.Hosts.AddRange(hostnames.Select(item => new TmHost { Hostname=item,Ip=string.Empty}));
            await context.SaveChangesAsync();
        }
    }
}
