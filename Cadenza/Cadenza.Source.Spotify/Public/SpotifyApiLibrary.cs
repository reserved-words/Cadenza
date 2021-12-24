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

        foreach (var item in albumsResponse.items)
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

        var playlistsResponse = await _api.GetUserPlaylists();

        var playlistArtistName = "Various Artists";
        var playlistArtistId = _idGenerator.GenerateArtistId(playlistArtistName);

        foreach (var item in playlistsResponse.items)
        {
            // if all tracks by same artist, change to that artist

            var albumArtistInfo = new ArtistInfo
            {
                Id = playlistArtistId,
                Name = playlistArtistName,
                Grouping = Grouping.None
            };

            var albumInfo = new AlbumInfo
            {
                Id = item.id,
                ArtistId = albumArtistInfo.Id,
                ArtistName = albumArtistInfo.Name,
                Title = item.name,
                ReleaseType = ReleaseType.Playlist,
                Year = "",
                ArtworkUrl = item.images.FirstOrDefault()?.url,
                DiscCount = 1
            };

            library.Artists.Add(albumArtistInfo);
            library.Albums.Add(albumInfo);

            var tracks = await _api.GetPlaylistTracks(item.id);

            foreach (var playlistItem in tracks.items)
            {
                var trackArtist = playlistItem.track.artists.First();
                var trackArtistInfo = GetArtistInfo(trackArtist);

                library.Artists.Add(trackArtistInfo);

                var trackInfo = GetTrackInfo(playlistItem, trackArtistInfo, item.id);
                var albumTrack = GetPlaylistTrack(item.id, playlistItem);

                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
            }
        }

        return library;
    }

    private static TrackInfo GetTrackInfo(SpotifyApiAlbumTracksItem track, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Id = track.uri,
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
            TrackId = track.uri,
            AlbumId = albumId,
            Position = new AlbumTrackPosition(track.disc_number, track.track_number)
        };
    }

    private static TrackInfo GetTrackInfo(SpotifyApiPlaylistItem item, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Id = item.track.uri,
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
            TrackId = item.track.uri,
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

        artistInfo.Id = _idGenerator.GenerateArtistId(artistInfo.Name);
        artistInfo.AddSourceId(LibrarySource.Spotify, artist.id);

        return artistInfo;
    }
}