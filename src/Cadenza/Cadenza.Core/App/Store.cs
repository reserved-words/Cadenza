using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Cadenza.Core;

public class Store : IStoreGetter, IStoreSetter
{
    private readonly IJSRuntime _js;

    public Store(IJSRuntime js)
    {
        _js = js;
    }

    public async Task Clear(StoreKey key)
    {
        await _js.InvokeVoidAsync("setStoredValue", key.ToString(), "");
    }

    public async Task<StoredValue<T>> GetValue<T>(StoreKey key)
    {
        var json = await _js.InvokeAsync<string>("getStoredValue", key.ToString());
        return string.IsNullOrWhiteSpace(json)
            ? null
            : JsonConvert.DeserializeObject<StoredValue<T>>(json);
    }

    public async Task SetValue<T>(StoreKey key, T value)
    {
        var storedValue = new StoredValue<T>(value);
        var json = JsonConvert.SerializeObject(storedValue);
        await _js.InvokeVoidAsync("setStoredValue", key.ToString(), json);
    }
}

public class StoredValue<T>
{
    public StoredValue()
    {

    }

    public StoredValue(T value)
    {
        Value = value;
        Updated = DateTime.Now;
    }

    public T Value { get; set; }
    public DateTime Updated { get; set; }
}
