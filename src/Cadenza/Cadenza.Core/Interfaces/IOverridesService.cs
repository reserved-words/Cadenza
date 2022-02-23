using Cadenza.Domain;

namespace Cadenza.Core.Interfaces;

public interface IOverridesService
{
    Task<List<ItemPropertyUpdate>> GetOverrides();
    Task<bool> RemoveOverride(string id, ItemProperty property);
}
