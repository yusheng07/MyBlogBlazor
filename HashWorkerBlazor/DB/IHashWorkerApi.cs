using HashWorkerBlazor.Models;

namespace HashWorkerBlazor.DB
{
    public interface IHashWorkerApi
    {
        Task<List<ListItem>> GetListItemsAsync();        
        Task<(bool isOk, string msg)> ResendListAsync(int listIdx);
        Task<(bool isOk, string msg)> SendListAsync(string account, string hashListJson,string checkHash, int hashCount);
    }
}
