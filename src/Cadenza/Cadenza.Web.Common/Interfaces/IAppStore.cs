namespace Cadenza.Web.Common.Interfaces;

public interface IAppStore
{
    Task Clear(StoreKey key);
    Task SetValue<T>(StoreKey key, T value, int? expiresInSeconds = null);
    Task<StoredValue<T>> GetValue<T>(StoreKey key);
    Task<StoredValue<T>> AwaitValue<T>(StoreKey storeKey, int timeoutSeconds, CancellationToken cancellationToken);
}
