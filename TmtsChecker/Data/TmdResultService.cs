using TmtsChecker.Models;
using static TmtsChecker.Models.ScanResult;

namespace TmtsChecker.Data
{
    public class TmdResultService : IScanResultService
    {
        public List<ScanResult> ScanItemList { get; private set; }
        private decimal sizeThreshold => 0.1M; //MB

        //public event Action OnChange;
        public TmdResultService()
        {
            ScanItemList = new List<ScanResult>();
        }

        public async Task<List<ScanResult>> ScanFolderAsync(string folderPath, string scannerName, string resultExt)
        {
            ScanItemList.Clear();
            //
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("folderPath is not exists!");
                return ScanItemList;
            }
            //
            foreach (var txtItem in new DirectoryInfo(folderPath).GetFiles("*.txt", SearchOption.AllDirectories))
            {
                //take last 100 lines
                var lines = await File.ReadAllLinesAsync(txtItem.FullName);
                //
                ScanResult currScanResult = new ScanResult {
                        Hostname = txtItem.Name.Substring(0, txtItem.Name.LastIndexOf(txtItem.Extension)),
                        Status = ScanStatus.Init,
                        IsComplete = false };
                //read txt from the end
                for (int i = lines.Count()-1; i >= 0 ; i--)
                {
                    string lineContext= lines.ElementAt(i);
                    if (string.IsNullOrWhiteSpace(lineContext)) { continue; }
                    //get IP
                    if (lineContext.Contains("IP:"))
                    {
                        currScanResult.Ip = lineContext.Substring(lineContext.IndexOf("IP:") + 4);
                        if (currScanResult.Status != ScanStatus.Init)
                        {
                            break; //already got Status, stop the loop
                        }
                    }
                    //get Status
                    if (currScanResult.Status == ScanStatus.Init)
                    {
                        if (lineContext.Contains($"start {scannerName}"))
                        {
                            currScanResult.Status = ScanStatus.Scanning;
                        }
                        else if (lineContext.Contains("compression"))
                        {
                            currScanResult.Status = ScanStatus.Compressing;
                        }
                        else if (lineContext.Contains("start upload"))
                        {
                            currScanResult.Status = ScanStatus.Uploading;
                        }
                        else if (lineContext.Contains("move file failed")
                                    || lineContext.Contains("upload file failed"))
                        {
                            currScanResult.Status = ScanStatus.UploadFailed;
                        }
                        else if (lineContext.Contains("finish upload"))
                        {
                            string zipPath = Path.Combine(txtItem.DirectoryName, currScanResult.Hostname + resultExt);                            
                            //check the zip file is exitst or not
                            if (File.Exists(zipPath))
                            {
                                FileInfo zipFileInfo = new FileInfo(zipPath);
                                currScanResult.FinishTime = zipFileInfo.LastWriteTime;
                                decimal zipSize = Math.Round((decimal)zipFileInfo.Length / (1024 * 1024), 2); //in MB
                                if (zipSize >= sizeThreshold) //file size OK!
                                {
                                    currScanResult.Status = ScanStatus.Complete;
                                    currScanResult.ResultSize = zipSize;
                                    currScanResult.IsComplete = true;
                                }
                                else //file size Error!
                                {
                                    currScanResult.Status = ScanStatus.SizeError;
                                }
                            }
                            else
                            {
                                currScanResult.Status = ScanStatus.UploadFailed;
                            }
                        }
                    }
                }
                //
                ScanItemList.Add(currScanResult);
            }
            return ScanItemList;
        }
    }
}
