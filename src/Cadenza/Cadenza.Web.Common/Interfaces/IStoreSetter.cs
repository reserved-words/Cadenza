namespace Cadenza.Web.Common.Interfaces;

public interface IStoreSetter
{
    Task Clear(StoreKey key);
    Task SetValue<T>(StoreKey key, T value, int? expiresInSeconds = null);
}
