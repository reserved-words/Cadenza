using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.Common.Interop;

public interface IStoreSetter
{
    Task Clear(StoreKey key);
    Task SetValue<T>(StoreKey key, T value, int? expiresInSeconds = null);
}
