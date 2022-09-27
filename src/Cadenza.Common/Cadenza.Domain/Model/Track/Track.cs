using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Model.Track;

public class Track
{
    public LibrarySource Source { get; set; }
    public string Id { get; set; }
    public string ArtistId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }
    public string AlbumId { get; set; }
}