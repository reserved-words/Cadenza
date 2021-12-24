namespace Cadenza.Source.Spotify;

public class SpotifyLibrary : ISourceRepository
{
    private readonly ILibrary _library;
    private readonly ISpotifyLibraryApi _api;
    private readonly IIdGenerator _idGenerator;

    public SpotifyLibrary(ILibrary library, ISpotifyLibraryApi api, IIdGenerator idGenerator)
    {
        _library = library;
        _api = api;
        _idGenerator = idGenerator;
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var artists = await _library.GetAlbumArtists();
        var result = new List<ArtistInfo>();
        foreach (var artist in artists)
        {
            var artistInfo = await _library.GetAlbumArtist(artist.Id);
            // this includes albums, don't need them at this point
            result.Add(artistInfo.Artist);
        }
        return result;
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums()
    {
        var artists = await _library.GetAlbumArtists();
        var result = new List<AlbumInfo>();
        foreach (var artist in artists)
        {
            var artistInfo = await _library.GetAlbumArtist(artist.Id);
            foreach (var album in artistInfo.Albums)
            {
                album.Album.Source = LibrarySource.Spotify;
                result.Add(album.Album);
            }
        }
        return result;
    }

    public async Task<PlayingTrack> GetTrack(string id)
    {
        var track = await _library.GetTrack(id);

        return new PlayingTrack
        {
            Id = id,
            Source = LibrarySource.Spotify,
            DurationSeconds = track.Track.DurationSeconds,
            Title = track.Track.Title,
            Artist = track.Artist.Name,
            AlbumTitle = track.Album.Title,
            AlbumArtist = track.Album.ArtistName,
            ArtworkUrl = track.Album.ArtworkUrl,
            ReleaseType = track.Album.ReleaseType,
            Year = track.Track.Year
        };
    }

    public async Task<List<string>> GetAlbumTracks(string artistId, string albumId)
    {
        var albumArtist = await _library.GetAlbumArtist(artistId);

        var album = albumArtist.Albums
            .Single(a => a.Album.Id == albumId);

        //if (album.Album.ReleaseType == ReleaseType.Playlist && !album.AlbumTracks.Any())
        //{
        //    var playlistTracks = await _api.GetPlaylistTracks(albumId);

        //    foreach (var item in playlistTracks.items)
        //    {
        //        var artist = item.track.artists.First();

        //        album.AlbumTracks
        //            .Add(new AlbumTrack
        //            {
        //                Track = new Track
        //                {
        //                    Id = item.track.uri,
        //                    Title = item.track.name,
        //                    DurationSeconds = item.track.duration_ms / 1000,
        //                    ArtistId = _idGenerator.GenerateArtistId(artist.name),
        //                    ArtistName = artist.name,
        //                    AlbumId = albumId,
        //                    Source = LibrarySource.Spotify
        //                },
        //                Position = new AlbumTrackPosition(1, item.track.track_number)
        //            });


        //    }
        //}

        return album
            .AlbumTracks
            .Select(t => t.Track.Id)
            .ToList();
    }

    public async Task<List<string>> GetArtistTracks(string id)
    {
        var tracks = await _library.GetAllTracks();

        return tracks
            .Where(t => t.ArtistId == id)
            .Select(t => t.Id)
            .ToList();
    }

    public async Task<List<string>> GetAllTracks()
    {
        var tracks = await _library.GetAllTracks();

        return tracks
            .Select(t => t.Id)
            .ToList();
    }
}

