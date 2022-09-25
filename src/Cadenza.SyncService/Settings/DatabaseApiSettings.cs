namespace Cadenza.SyncService.Settings;

public class DatabaseApiSettings
{
    public string BaseUrl { get; set; }
    public DatabaseApiEndpoints Endpoints { get; set; }
}

public class DatabaseApiEndpoints
{
    public string AddTrack { get; set; }
    public string GetAllTracks { get; set; }
    public string GetTracksByArtist { get; set; }
    public string GetTracksByAlbum { get; set; }
    public string GetUpdates { get; set; }
    public string MarkUpdated { get; set; }
    public string RemoveTrack { get; set; }
}