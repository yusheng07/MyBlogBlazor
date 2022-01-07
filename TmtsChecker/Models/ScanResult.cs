namespace TmtsChecker.Models
{
    public class ScanResult
    {
        public enum ScanStatus { Init, Scaning, Compressing, Uploading, Complete, SizeError, UploadFailed }
        public string Hostname { get; set; }
        public string Ip { get; set; }
        public ScanStatus Status { get; set; }
        public decimal ResultSize { get; set; }
        public bool IsComplete { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
