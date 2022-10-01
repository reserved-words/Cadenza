namespace Cadenza.Interop;

public class StoreInterop : IStore
{
    private readonly IJSRuntime _js;

    public StoreInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SetValue(string key, string value)
    {
        await _js.InvokeVoidAsync("setStoredValue", key, value);
    }

    public async Task<string> GetValue(string key)
    {
        return await _js.InvokeAsync<string>("getStoredValue", key);
    }
}
