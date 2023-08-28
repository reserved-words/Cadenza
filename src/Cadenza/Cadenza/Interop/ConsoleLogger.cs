namespace Cadenza.Interop;

public class ConsoleLogger : IDebugLogger
{
    private readonly IJSRuntime _js;

    public ConsoleLogger(IJSRuntime js)
    {
        _js = js;
    }

    public async Task DisplayInfo(string message)
    {
        await _js.InvokeVoidAsync("alert", message);
    }

    public async Task LogError(Exception ex)
    {
        await _js.InvokeVoidAsync("console.log", ex.Message);
        await _js.InvokeVoidAsync("console.log", ex.StackTrace);
    }

    public async Task LogInfo(string message)
    {
        await _js.InvokeVoidAsync("console.log", message);
    }
}
