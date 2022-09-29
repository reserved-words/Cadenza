using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Common.Interop;

public interface IStoreGetter
{
    Task<StoredValue<T>> GetValue<T>(StoreKey key);

    Task<StoredValue<T>> AwaitValue<T>(StoreKey storeKey, int timeoutSeconds, CancellationToken cancellationToken);
}
