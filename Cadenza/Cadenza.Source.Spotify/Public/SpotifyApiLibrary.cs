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
        var response = await _api.GetUserAlbums();

        var library = new StaticLibrary();

        foreach (var item in response.items)
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

                var trackInfo = GetTrackInfo(track, trackArtistInfo);
                var albumTrack = GetAlbumTrack(album.id, track);

                library.Tracks.Add(trackInfo);
                library.AlbumTrackLinks.Add(albumTrack);
            }
        }

        return library;
    }

    private static TrackInfo GetTrackInfo(SpotifyApiAlbumTracksItem track, ArtistInfo trackArtist)
    {
        return new TrackInfo
        {
            Id = track.uri,
            Title = track.name,
            DurationSeconds = track.duration_ms / 1000,
            ArtistId = trackArtist.Id,
            ArtistName = trackArtist.Name
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
            ImageUrl = album.images.FirstOrDefault()?.url,
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

    private ArtistInfo GetArtistInfo(SpotifyApiAlbumArtist artist)
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