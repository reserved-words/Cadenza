using Cadenza.Domain;
using Cadenza.Library;
using Cadenza.Spotify.API.Interfaces;
using Cadenza.Spotify.API.Model.Albums;
using Cadenza.Spotify.API.Model.Common;
using Cadenza.Spotify.API.Model.Playlists;
using Cadenza.Utilities;

namespace Cadenza.Spotify.Libraries;

public class ApiLibrary : IStaticLibrary
{
    private readonly IIdGenerator _idGenerator;
    private readonly IApiCaller _api;

    public ApiLibrary(IApiCaller api, IIdGenerator idGenerator)
    {
        _api = api;
        _idGenerator = idGenerator;
    }

    public async Task<FullLibrary> Get()
    {
        var library = new FullLibrary();

        var albumsResponse = await _api.GetUserAlbums();

        foreach (var item in albumsResponse)
        {
            var album = item.album;
            var albumArtist = AddArtist(library, album.artists);
            var albumInfo = GetAlbumInfo(album, albumArtist);
            library.Albums.Add(albumInfo);

            foreach (var track in album.tracks.items)
            {
                var trackArtistInfo = AddArtist(library, track.artists);
                var trackInfo = GetTrackInfo(track, trackArtistInfo, album.id);
                var albumTrack = GetAlbumTrack(album.id, track);
                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
            }
        }

        var playlists = await _api.GetUserPlaylists();

        foreach (var playlist in playlists)
        {
            var tracks = await _api.GetPlaylistTracks(playlist.id);

            var trackArtists = new List<ArtistInfo>();

            for (var i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];
                var trackArtistInfo = AddArtist(library, track.track.artists);
                var trackInfo = GetTrackInfo(track, trackArtistInfo, playlist.id);
                var albumTrack = GetPlaylistTrack(playlist.id, track, i + 1);
                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
                trackArtists.Add(trackArtistInfo);
            }

            var distinctArtists = trackArtists
                .Select(a => a.Id)
                .Distinct()
                .Count();

            var albumArtistInfo = distinctArtists > 1
                ? AddArtist(library, "Various Artists")
                : trackArtists.First();

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

    private ArtistInfo AddArtist(FullLibrary library, string name)
    {
        var id = GetUniversalId(name);
        var artistInfo = new ArtistInfo
        {
            Id = id,
            Name = name,
            Grouping = Grouping.None
        };

        if (!library.Artists.Any(a => a.Id == artistInfo.Id))
        {
            library.Artists.Add(artistInfo);
        }

        return artistInfo;
    }

    private ArtistInfo AddArtist(FullLibrary library, List<SpotifyApiArtist> artists)
    {
        var artist = artists.First();
        var artistInfo = GetArtistInfo(artist);
        if (!library.Artists.Any(a => a.Id == artistInfo.Id))
        {
            library.Artists.Add(artistInfo);
        }
        return artistInfo;
    }

    private static TrackInfo GetTrackInfo(SpotifyApiAlbumTracksItem track, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Source = LibrarySource.Spotify,
            Id = GetTrackId(track.id, albumId),
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
            TrackId = GetTrackId(track.id, albumId),
            AlbumId = albumId,
            Position = new AlbumTrackPosition(track.disc_number, track.track_number)
        };
    }

    private static TrackInfo GetTrackInfo(SpotifyApiPlaylistItem item, ArtistInfo trackArtist, string albumId)
    {
        return new TrackInfo
        {
            Source = LibrarySource.Spotify,
            Id = GetTrackId(item.track.id, albumId),
            Title = item.track.name,
            DurationSeconds = item.track.duration_ms / 1000,
            ArtistId = trackArtist.Id,
            ArtistName = trackArtist.Name,
            AlbumId = albumId
        };
    }

    private AlbumTrackLink GetPlaylistTrack(string playlistId, SpotifyApiPlaylistItem item, int trackNo)
    {
        return new AlbumTrackLink
        {
            TrackId = GetTrackId(item.track.id, playlistId),
            AlbumId = playlistId,
            Position = new AlbumTrackPosition(1, trackNo)
        };
    }

    private ArtistInfo GetArtistInfo(SpotifyApiArtist artist)
    {
        return new ArtistInfo
        {
            Id = GetUniversalId(artist.name),
            Name = artist.name,
            Grouping = Grouping.None
        };
    }

    private static string GetTrackId(string trackId, string albumId)
    {
        return $"{trackId}|{albumId}";
    }

    private string GetUniversalId(string artistName)
    {
        return _idGenerator.GenerateId(artistName);
    }
}