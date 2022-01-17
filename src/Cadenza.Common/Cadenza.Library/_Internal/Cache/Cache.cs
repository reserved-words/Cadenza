using Cadenza.Domain;

namespace Cadenza.Library;

internal class Cache : ICache
{
    public Dictionary<string, ArtistInfo> Artists { get; } = new();
    public Dictionary<string, TrackInfo> Tracks { get; } = new();
    public Dictionary<string, AlbumInfo> Albums { get; } = new();

    public Dictionary<string, ArtistLinks> ArtistLinks { get; } = new();
    public Dictionary<string, AlbumLinks> AlbumLinks { get; } = new();
    public Dictionary<string, TrackLinks> TrackLinks { get; } = new();

    public List<string> AlbumArtists { get; } = new();
}