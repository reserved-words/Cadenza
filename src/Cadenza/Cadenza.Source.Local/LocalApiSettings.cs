namespace Cadenza.Source.Local;

public class LocalApiSettings : ApiOptions<LocalApiEndpoints>
{   
}

public class LocalApiEndpoints
{
    public string Albums { get; set; }
    public string Artists { get; set; }
    public string AllTracks { get; set; }
    public string AlbumTracks { get; set; }
    public string ArtistTracks { get; set; }
    public string Track { get; set; }
    public string FullTrack { get; set; }

    public string QueuedUpdates { get; set; }
    public string PlayTrackUrl { get; set; }
    public string UpdateAlbum { get; set; }
    public string UpdateArtist { get; set; }
    public string UpdateTrack { get; set; }
    public string UnqueueUpdate { get; set; }
}
