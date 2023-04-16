namespace Cadenza.Common.Domain.Model;

public class PlayTrack
{
    public LibrarySource Source { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public int AlbumId { get; set; }
}
