using Cadenza.Domain;

namespace Cadenza.Common;

public interface IOverridesService
{
    Task<bool> AddOverrides(List<ItemPropertyUpdate> overrides);
    Task<List<ItemPropertyUpdate>> GetOverrides();
    Task<bool> RemoveOverride(string id, ItemProperty property);
}
