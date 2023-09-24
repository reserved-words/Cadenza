using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Core.Utilities;

internal class Store : IAppStore
{
    private readonly IJsonConverter _converter;
    private readonly IStore _store;

    public Store(IStore store, IJsonConverter converter)
    {
        _store = store;
        _converter = converter;
    }

    public async Task<StoredValue<T>> AwaitValue<T>(StoreKey storeKey, int timeoutSeconds)
    {
        var startTime = DateTime.Now;
        var endTime = startTime.AddSeconds(timeoutSeconds);

        var token = await GetValue<T>(storeKey);

        while (token == null && DateTime.Now < endTime)
        {
            await Task.Delay(500);
            token = await GetValue<T>(storeKey);
        }

        return token;
    }

    public async Task Clear(StoreKey key)
    {
        await _store.SetValue(key.ToString(), "");
    }

    public async Task<StoredValue<T>> GetValue<T>(StoreKey key)
    {
        var json = await _store.GetValue(key.ToString());

        if (string.IsNullOrWhiteSpace(json))
            return null;

        return _converter.Deserialize<StoredValue<T>>(json);
    }

    public async Task SetValue<T>(StoreKey key, T value, int? expiresInSeconds = null)
    {
        var storedValue = new StoredValue<T>(value, expiresInSeconds);
        var json = _converter.Serialize(storedValue);
        await _store.SetValue(key.ToString(), json);
    }
}
