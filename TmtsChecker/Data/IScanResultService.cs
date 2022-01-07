using TmtsChecker.Models;

namespace TmtsChecker.Data
{
    public interface IScanResultService
    {
        List<ScanResult> ScanItemList { get; }

        //event Action OnChange;

        Task<List<ScanResult>> ScanFolderAsync(string folderPath, string scannerName, string resultExt);
    }
}
