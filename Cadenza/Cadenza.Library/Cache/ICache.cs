namespace Cadenza.Library;

public interface ICache
{
    List<string> AlbumArtists { get; }
    Dictionary<string, AlbumLinks> AlbumLinks { get; }
    Dictionary<string, AlbumInfo> Albums { get; }
    Dictionary<string, ArtistLinks> ArtistLinks { get; }
    Dictionary<string, ArtistInfo> Artists { get; }
    Dictionary<string, TrackLinks> TrackLinks { get; }
    Dictionary<string, TrackInfo> Tracks { get; }
}
