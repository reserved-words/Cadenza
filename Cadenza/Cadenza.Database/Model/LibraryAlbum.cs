using Cadenza.Common;

namespace Cadenza.Database;

public class LibraryAlbum
{
    public string Id { get; set; }
    public string Artist { get; set; }
    public string ArtistId { get; set; }
    public string Title { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public ReleaseTypeGroup Group { get; set; }
    public string Year { get; set; }
    public string Artwork { get; set; }
    public LibrarySource Source { get; set; }
}
