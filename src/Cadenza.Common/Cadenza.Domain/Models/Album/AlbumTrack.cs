namespace Cadenza.Domain;

public class AlbumTrack
{
    public Track Track { get; set; }
    public AlbumTrackPosition Position { get; set; } = new();
}
