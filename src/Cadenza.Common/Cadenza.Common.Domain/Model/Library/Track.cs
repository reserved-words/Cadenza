namespace Cadenza.Common.Domain.Model.Library;

public class Track
{
    public LibrarySource Source { get; set; }
    public int Id { get; set; }
    public string IdFromSource { get; set; }
    public int ArtistId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }
    public int AlbumId { get; set; }
}