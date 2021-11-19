namespace Cadenza.Player;

public interface IStoreGetter
{
    Task<string> GetValue(StoreKey key);
    Task<List<string>> GetValues(StoreKey key);
}