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
    public string GetRemovalRequests { get; set; }
    public string MarkRemovalErrored { get; set; }
    public string MarkRemovalDone { get; set; }
    public string GetTrackIdFromSource { get; set; }
    public string GetTracksByArtist { get; set; }
    public string GetTracksByAlbum { get; set; }
    public string GetUpdateRequests { get; set; }
    public string MarkUpdateErrored { get; set; }
    public string MarkUpdateDone { get; set; }
    public string RemoveTracks { get; set; }
}