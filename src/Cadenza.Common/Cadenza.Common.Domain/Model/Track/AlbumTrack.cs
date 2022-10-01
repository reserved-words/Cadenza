namespace Cadenza.Common.Domain.Model.Track;

public class AlbumTrack
{
    public string TrackId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }
    public string ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }
    public AlbumTrackPosition Position { get; set; }
}
