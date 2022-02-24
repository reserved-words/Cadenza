using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Cadenza.Core.App;

public class StoreInterop : IStoreGetter, IStoreSetter
{
    private readonly IJSRuntime _js;

    public StoreInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<StoredValue<T>> AwaitValue<T>(StoreKey storeKey, int timeoutSeconds, CancellationToken cancellationToken)
    {
        try
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddSeconds(timeoutSeconds);

            var token = await GetValue<T>(storeKey);

            while (token == null && DateTime.Now < endTime && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500);
                token = await GetValue<T>(storeKey);
            }

            return token;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task Clear(StoreKey key)
    {
        await _js.InvokeVoidAsync("setStoredValue", key.ToString(), "");
    }

    public async Task<StoredValue<T>> GetValue<T>(StoreKey key)
    {
        var json = await _js.InvokeAsync<string>("getStoredValue", key.ToString());

        if (string.IsNullOrWhiteSpace(json))
            return null;

        return JsonConvert.DeserializeObject<StoredValue<T>>(json);
    }

    public async Task SetValue<T>(StoreKey key, T value)
    {
        var storedValue = new StoredValue<T>(value);
        var json = JsonConvert.SerializeObject(storedValue);
        await _js.InvokeVoidAsync("setStoredValue", key.ToString(), json);
    }
}
