namespace Cadenza.Core;

public interface IStoreSetter
{
    Task Clear(StoreKey key);
    Task SetValue<T>(StoreKey key, T value);
}