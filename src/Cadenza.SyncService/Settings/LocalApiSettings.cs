namespace Cadenza.SyncService.Settings;

public class LocalApiSettings
{
    public string BaseUrl { get; set; }
    public LocalApiEndpoints Endpoints { get; set; }
}

public class LocalApiEndpoints
{
    public string GetTracks { get; set; }
    public string GetTrack { get; set; }
    public string UpdateTracks { get; set; }
}
