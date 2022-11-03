namespace Cadenza.API.Cache.Services;

internal class CacheContainer : ICacheContainer
{
    public Dictionary<string, PlayTrack> PlayTracks { get; } = new();
    public Dictionary<string, List<PlayTrack>> TagPlayTracks { get; } = new();

    public Dictionary<PlayerItemType, List<PlayerItem>> Items { get; } = new();
    public Dictionary<string, List<PlayerItem>> Tags { get; } = new();

    public Dictionary<string, TrackInfo> Tracks { get; } = new();
    public Dictionary<string, AlbumInfo> Albums { get; } = new();
    public Dictionary<string, ArtistInfo> Artists { get; } = new();
    public Dictionary<string, AlbumTrackLink> AlbumTracks { get; } = new();

    public Dictionary<Grouping, List<ArtistInfo>> ArtistsByGrouping { get; } = new();
    public Dictionary<string, List<ArtistInfo>> ArtistsByGenre { get; } = new();

    public Dictionary<string, List<AlbumInfo>> AlbumsByArtist { get; } = new();
    public Dictionary<string, List<TrackInfo>> TracksByArtist { get; } = new();
    public Dictionary<string, List<TrackInfo>> TracksByAlbum { get; } = new();

    public void Populate(FullLibrary library)
    {
        foreach (var artist in library.Artists)
        {
            if (string.IsNullOrWhiteSpace(artist.Genre))
            {
                artist.Genre = "None";
            }

            Artists.Cache(artist.Id, artist);
            ArtistsByGrouping.Cache(artist.Grouping, artist);
            ArtistsByGenre.Cache(artist.Genre, artist);

            var item = new SearchableArtist(artist);
            Items.Cache(PlayerItemType.Artist, item);
            Tags.Cache(artist.Tags, item);
            Items.Cache(PlayerItemType.Grouping, artist.Grouping.ToString(), () => new SearchableGrouping(artist.Grouping));
            Items.Cache(PlayerItemType.Genre, artist.Genre, () => new SearchableGenre(artist.Genre));

        }

        foreach (var album in library.Albums)
        {
            Albums.Cache(album.Id, album);
            AlbumsByArtist.Cache(album.ArtistId, album);

            var item = new SearchableAlbum(album);
            Items.Cache(PlayerItemType.Album, item);
            Tags.Cache(album.Tags, item);
        }

        foreach (var track in library.Tracks)
        {
            Tracks.Cache(track.Id, track);
            TracksByArtist.Cache(track.ArtistId, track);

            var item = new SearchableTrack(track, Albums[track.AlbumId]);
            Items.Cache(PlayerItemType.Track, item);
            Tags.Cache(track.Tags, item);

            var artist = Artists[track.ArtistId];
            var album = Albums[track.AlbumId];

            var playTrack = new PlayTrack
            {
                Id = track.Id,
                Title = track.Title,
                ArtistId = track.ArtistId,
                AlbumId = track.AlbumId,
                Source = track.Source
            };

            PlayTracks.Add(playTrack.Id, playTrack);
            TagPlayTracks.Cache(track, artist, album, playTrack);
        }

        var albumTracks = library.AlbumTracks
            .OrderBy(at => at.AlbumId)
            .ThenBy(at => at.DiscNo)
            .ThenBy(at => at.TrackNo)
            .ToList();

        foreach (var albumTrack in albumTracks)
        {
            AlbumTracks.Cache(albumTrack.TrackId, albumTrack);
            TracksByAlbum.Cache(albumTrack.AlbumId, Tracks[albumTrack.TrackId]);
        }
    }
}
