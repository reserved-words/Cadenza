namespace Cadenza.Core;

public interface IStoreSetter
{
    Task<bool> SetValue(StoreKey key, object value);
    Task<bool> SetValue<T>(StoreKey key, T value) where T : class;
    Task<bool> SetValues<T>(StoreKey key, List<T> values);
}