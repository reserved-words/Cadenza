using Cadenza.Domain;

namespace Cadenza.Source.Spotify;

public class SpotifyLibrary : ISourceRepository
{
    private readonly ILibrary _library;

    public SpotifyLibrary(ILibrary library, ISpotifyLibraryApi api, IIdGenerator idGenerator)
    {
        _library = library;
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var artists = await _library.GetAlbumArtists();
        var result = new List<ArtistInfo>();
        foreach (var artist in artists)
        {
            var artistInfo = await _library.GetAlbumArtist(artist.Id);
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
        return await _library.GetTrack(id);
    }

    public async Task<FullTrack> GetFullTrack(string id)
    {
        return await _library.GetFullTrack(id);
    }

    public async Task<List<string>> GetAlbumTracks(string artistId, string albumId)
    {
        var albumArtist = await _library.GetAlbumArtist(artistId);

        var album = albumArtist.Albums
            .Single(a => a.Album.Id == albumId);

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

