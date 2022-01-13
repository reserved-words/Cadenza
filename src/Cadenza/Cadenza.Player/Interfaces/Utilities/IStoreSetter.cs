namespace Cadenza.Player;

public interface IStoreSetter
{
    Task<bool> SetValue(StoreKey key, object value);
    Task<bool> SetValues<T>(StoreKey key, List<T> values);
}