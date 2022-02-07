using Cadenza.Domain;

namespace Cadenza.API.Core.Common
{
    public interface IOverridesService
    {
        Task<bool> AddOverrides(List<ItemPropertyUpdate> updates);
        Task<List<ItemPropertyUpdate>> GetOverrides();
        Task<bool> RemoveOverride(string id, ItemProperty property);
    }
}