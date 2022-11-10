using TmtsChecker.Models;

namespace TmtsChecker.DB
{
    public interface ITmtsDataApi
    {
        Task<List<TmHost>> GetHostsAsync();
        Task ReplaceHostsAsync(List<string> hostnames);
    }
}
