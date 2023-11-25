
namespace Cadenza.SyncService.Settings;

internal class ServiceSettings
{
    public int SyncFrequencySeconds { get; set; }
    public int ScrobbleFrequencySeconds { get; set; }
    public int ScrobbleSyncFrequencySeconds { get; set; }
    public int UpdateFrequencySeconds { get; set; }
}