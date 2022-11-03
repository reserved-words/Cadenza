namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheContainer
{
    Dictionary<string, AlbumInfo> Albums { get; }
    Dictionary<string, List<AlbumInfo>> AlbumsByArtist { get; }
    Dictionary<string, AlbumTrackLink> AlbumTracks { get; }
    Dictionary<string, ArtistInfo> Artists { get; }
    Dictionary<string, List<ArtistInfo>> ArtistsByGenre { get; }
    Dictionary<Grouping, List<ArtistInfo>> ArtistsByGrouping { get; }
    Dictionary<PlayerItemType, List<PlayerItem>> Items { get; }
    Dictionary<string, PlayTrack> PlayTracks { get; }
    Dictionary<string, List<PlayTrack>> TagPlayTracks { get; }
    Dictionary<string, List<PlayerItem>> Tags { get; }
    Dictionary<string, TrackInfo> Tracks { get; }
    Dictionary<string, List<TrackInfo>> TracksByAlbum { get; }
    Dictionary<string, List<TrackInfo>> TracksByArtist { get; }

    void Populate(FullLibrary library);
}