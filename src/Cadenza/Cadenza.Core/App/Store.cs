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

    public async Task<string> GetString(StoreKey key)
    {
        return await _js.InvokeAsync<string>("getStoredValue", key.ToString());
    }

    public async Task<T> GetValue<T>(StoreKey key) where T : class
    {
        var json = await GetString(key);
        return json == null
            ? null
            : JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<int?> GetInt(StoreKey key)
    {
        var value = await GetString(key);
        return int.TryParse(value, out int result)
            ? result
            : null;
    }

    public async Task<List<string>> GetList(StoreKey key)
    {
        var value = await GetString(key);
        return value.Split("|").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
    }

    public async Task<bool> SetValue(StoreKey key, object value)
    {
        var isValueChanged = await IsValueChanged(key, value);

        if (!isValueChanged)
            return false;

        await SetValue(key, value?.ToString());
        return true;
    }

    public async Task<bool> SetValue<T>(StoreKey key, T value) where T : class
    {
        var json = value == null
            ? null
            : JsonConvert.SerializeObject(value);

        await SetValue(key, json);
        return true;
    }

    public async Task<bool> SetValues<T>(StoreKey key, List<T> values)
    {
        var isValueChanged = await IsValueChanged(key, values);

        if (!isValueChanged)
            return false;

        var value = values == null
            ? null
            : string.Join("|", values);

        await SetValue(key, value);
        return true;
    }

    private async Task<bool> IsValueChanged(StoreKey key, object newValue)
    {
        var currentValue = await GetString(key);

        if (currentValue == null)
        {
            return newValue != null;
        }

        return !currentValue.Equals(newValue);
    }

    private async Task<bool> IsValueChanged<T>(StoreKey key, List<T> newValue)
    {
        var currentValue = await GetList(key);

        if (currentValue == null)
        {
            return newValue != null;
        }

        if (newValue == null)
        {
            return currentValue != null;
        }

        return currentValue.Count != newValue.Count
            || !newValue.All(s => currentValue.Contains(s.ToString()));
    }

    private async Task SetValue(StoreKey key, string value)
    {
        await _js.InvokeVoidAsync("setStoredValue", key.ToString(), value);
    }
}
