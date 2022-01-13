namespace Cadenza.Source.Spotify;

public class SpotifyApiLibrary : IStaticSource
{
    private readonly IIdGenerator _idGenerator;
    private readonly ISpotifyLibraryApi _api;

    public SpotifyApiLibrary(ISpotifyLibraryApi api, IIdGenerator idGenerator)
    {
        _api = api;
        _idGenerator = idGenerator;
    }

    public async Task<StaticLibrary> GetStaticLibrary()
    {
        var library = new StaticLibrary();

        var albumsResponse = await _api.GetUserAlbums();

        foreach (var item in albumsResponse)
        {
            var album = item.album;
            var albumArtist = album.artists.First();

            var albumArtistInfo = GetArtistInfo(albumArtist);
            var albumInfo = GetAlbumInfo(album, albumArtistInfo);

            library.Artists.Add(albumArtistInfo);
            library.Albums.Add(albumInfo);

            foreach (var track in album.tracks.items)
            {
                var trackArtist = track.artists.First();

                var trackArtistInfo = trackArtist.id == albumArtist.id
                    ? albumArtistInfo
                    : GetArtistInfo(trackArtist);

                if (trackArtistInfo.Id != albumArtistInfo.Id)
                {
                    library.Artists.Add(trackArtistInfo);
                }

                var trackInfo = GetTrackInfo(track, trackArtistInfo, album.id);
                var albumTrack = GetAlbumTrack(album.id, track);

                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
            }
        }

        var playlists = await _api.GetUserPlaylists();

        foreach (var playlist in playlists)
        {
            // This will only get first 50 tracks, will need to redo to get all

            var tracks = await _api.GetPlaylistTracks(playlist.id);

            var trackArtists = new List<ArtistInfo>();

            foreach (var track in tracks)
            {
                var trackArtist = track.track.artists.First();

                var trackArtistInfo = trackArtists
                    .SingleOrDefault(a => a.Id == GetUniversalId(trackArtist.name));

                if (trackArtistInfo == null)
                {
                    trackArtistInfo = GetArtistInfo(trackArtist);
                    trackArtists.Add(trackArtistInfo);
                    library.Artists.Add(trackArtistInfo);
                }

                var trackInfo = GetTrackInfo(track, trackArtistInfo, playlist.id);
                var albumTrack = GetPlaylistTrack(playlist.id, track);

                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
            }

            ArtistInfo albumArtistInfo;

            if (trackArtists.Count > 1)
            {
                var playlistArtistName = "Various Artists";
                var playlistArtistId = GetUniversalId(playlistArtistName);

                albumArtistInfo = new ArtistInfo
                {
                    Id = playlistArtistId,
                    Name = playlistArtistName,
                    Grouping = Grouping.None
                };

                library.Artists.Add(albumArtistInfo);
            }
            else
            {
                albumArtistInfo = trackArtists.Single();
            }

            var albumInfo = new AlbumInfo
            {
                Source = LibrarySource.Spotify,
                Id = playlist.id,
                ArtistId = albumArtistInfo.Id,
                ArtistName = albumArtistInfo.Name,
                Title = playlist.name,
                ReleaseType = ReleaseType.Playlist,
                Year = "",
                ArtworkUrl = playlist.images.FirstOrDefault()?.url,
                DiscCount = 1,
                TrackCounts = new List<int> { playlist.tracks.total }
            };

            library.Albums.Add(albumInfo);
        }

        return library;
    }

    private static TrackInfo GetTrackInfo(SpotifyApiAlbumTracksItem track, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Source = LibrarySource.Spotify,
            Id = track.id,
            Title = track.name,
            DurationSeconds = track.duration_ms / 1000,
            ArtistId = trackArtist.Id,
            ArtistName = trackArtist.Name,
            AlbumId = albumId
        };
    }

    private AlbumInfo GetAlbumInfo(SpotifyApiAlbum album, ArtistInfo albumArtist)
    {
        return new AlbumInfo
        {
            Source = LibrarySource.Spotify,
            Id = album.id,
            ArtistId = albumArtist.Id,
            ArtistName = albumArtist.Name,
            Title = album.name,
            ReleaseType = ReleaseType.Album, // check - possibly Spotify has ones that correspond
            Year = GetReleaseYear(album),
            ArtworkUrl = album.images.FirstOrDefault()?.url,
            DiscCount = album.tracks.items.Max(t => t.disc_number)
        };
    }

    private string GetReleaseYear(SpotifyApiAlbum album)
    {
        return DateTime.TryParse(album.release_date, out DateTime result)
            ? result.Year.ToString()
            : "";
    }

    private AlbumTrackLink GetAlbumTrack(string albumId, SpotifyApiAlbumTracksItem track)
    {
        return new AlbumTrackLink
        {
            TrackId = track.id,
            AlbumId = albumId,
            Position = new AlbumTrackPosition(track.disc_number, track.track_number)
        };
    }

    private static TrackInfo GetTrackInfo(SpotifyApiPlaylistItem item, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Source = LibrarySource.Spotify,
            Id = item.track.id,
            Title = item.track.name,
            DurationSeconds = item.track.duration_ms / 1000,
            ArtistId = trackArtist.Id,
            ArtistName = trackArtist.Name,
            AlbumId = albumId
        };
    }

    private AlbumTrackLink GetPlaylistTrack(string playlistId, SpotifyApiPlaylistItem item)
    {
        return new AlbumTrackLink
        {
            TrackId = item.track.id,
            AlbumId = playlistId,
            Position = new AlbumTrackPosition(1, item.track.track_number)
        };
    }

    private ArtistInfo GetArtistInfo(SpotifyApiArtist artist)
    {
        var artistInfo = new ArtistInfo
        {
            Name = artist.name,
            Grouping = Grouping.None
        };

        artistInfo.Id = GetUniversalId(artistInfo.Name);

        return artistInfo;
    }

    private string GetUniversalId(string artistName)
    {
        return _idGenerator.GenerateArtistId(artistName);
    }
}