namespace Cadenza.API.Core.Services.Cache;

internal class CacheContainer
{
    public readonly Dictionary<string, PlayTrack> PlayTracks = new();
    public readonly Dictionary<string, List<PlayTrack>> TagPlayTracks = new();

    public readonly Dictionary<PlayerItemType, List<PlayerItem>> Items = new();
    public readonly Dictionary<string, List<PlayerItem>> Tags = new();

    public readonly Dictionary<string, TrackInfo> Tracks = new();
    public readonly Dictionary<string, AlbumInfo> Albums = new();
    public readonly Dictionary<string, ArtistInfo> Artists = new();
    public readonly Dictionary<string, AlbumTrackLink> AlbumTracks = new();

    public readonly Dictionary<Grouping, List<ArtistInfo>> ArtistsByGrouping = new();
    public readonly Dictionary<string, List<ArtistInfo>> ArtistsByGenre = new();

    public readonly Dictionary<string, List<AlbumInfo>> AlbumsByArtist = new();
    public readonly Dictionary<string, List<TrackInfo>> TracksByArtist = new();
    public readonly Dictionary<string, List<TrackInfo>> TracksByAlbum = new();

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
