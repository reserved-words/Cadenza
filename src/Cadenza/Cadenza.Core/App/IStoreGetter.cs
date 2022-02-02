namespace Cadenza.Core;

public interface IStoreGetter
{
    Task<StoredValue<T>> GetValue<T>(StoreKey key);
}