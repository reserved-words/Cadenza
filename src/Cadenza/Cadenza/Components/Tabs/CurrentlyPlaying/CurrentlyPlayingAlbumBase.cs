namespace Cadenza;

public class CurrentlyPlayingAlbumBase : ComponentBase
{
    [Parameter]
    public TrackFull Track { get; set; }

    public string DiscPosition => $"Disc {Track.AlbumTrack.Position.DiscNo} of {Track.Album.DiscCount}";
    public string TrackPosition => $"Track {Track.AlbumTrack.Position.TrackNo} of {Track.Album.TrackCounts[Track.AlbumTrack.Position.DiscNo-1]}";
}