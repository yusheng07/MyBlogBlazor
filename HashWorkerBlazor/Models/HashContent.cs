namespace HashWorkerBlazor.Models
{
    public class HashContent
    {
        public int No { get; set; }
        public string Hash { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public ListItem? ListItem { get; set; }
    }
}
