namespace Cadenza.Core;

public interface IStoreGetter
{
    Task<string> GetString(StoreKey key);
    Task<int?> GetInt(StoreKey key);
    Task<T> GetValue<T>(StoreKey key) where T : class;
    Task<List<string>> GetList(StoreKey key);
}