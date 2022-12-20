using Microsoft.JSInterop;

namespace HashWorkerBlazor.JSInterop
{
    public class FileapiJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public FileapiJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               //"import", "./_content/HashWorkerBlazor/yys.fileapi.js").AsTask());
               "import", "./yys.fileapi.js").AsTask());
        }

        public async ValueTask<string> GetMd5(object file)
        {
            var module = await moduleTask.Value;            
            return await module.InvokeAsync<string>("getMd5", file);
        }

        public async ValueTask<string> Prompt(string message)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
