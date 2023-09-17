namespace Cadenza.Common.Domain.Model;

public class PlayTrack
{
    public LibrarySource Source { get; set; }
    public int Id { get; set; }
    public string IdFromSource { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public int AlbumId { get; set; }
    public int Duration { get; set; }
}
