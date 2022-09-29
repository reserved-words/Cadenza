namespace Cadenza.Web.Common.Interfaces;

public interface IStoreGetter
{
    Task<StoredValue<T>> GetValue<T>(StoreKey key);

    Task<StoredValue<T>> AwaitValue<T>(StoreKey storeKey, int timeoutSeconds, CancellationToken cancellationToken);
}
