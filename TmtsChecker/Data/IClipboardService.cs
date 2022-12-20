namespace TmtsChecker.Data
{
    public interface IClipboardService
    {
        Task CopyToClipboard(string text);
    }
}
