namespace Cadenza.Common;

public interface IOverridesService
{
    Task<bool> AddOverrides(List<MetaDataUpdate> overrides);
    Task<List<MetaDataUpdate>> GetOverrides();
    Task<bool> RemoveOverride(string id, ItemProperty property);
}
