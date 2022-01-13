namespace Cadenza.Common;

public class LibraryArtistDetails : LibraryArtist
{
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public Dictionary<ReleaseTypeGroup, List<LibraryAlbum>> Releases { get; set; } = new Dictionary<ReleaseTypeGroup, List<LibraryAlbum>>();
    public List<Link> Links { get; set; }
}
