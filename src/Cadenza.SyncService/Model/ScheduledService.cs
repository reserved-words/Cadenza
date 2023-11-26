namespace Cadenza.SyncService.Model;

internal class ScheduledService
{
    public IService Service { get; set; }
    public int FrequencySeconds { get; set; }
    public DateTime? LastRun { get; set; }
}
