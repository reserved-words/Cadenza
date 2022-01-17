using Cadenza.Domain;

namespace Cadenza.Library;

internal abstract class SourceLibrary : ILibrary
{
    private readonly ILibrary _baseLibrary;

    public SourceLibrary(ILibrary baseLibrary, LibrarySource source)
    {
        _baseLibrary = baseLibrary;
        Source = source;
    }

    public LibrarySource Source { get; private set; }

    public async Task<ArtistFull> GetAlbumArtist(string id)
    {
        var artist = await _baseLibrary.GetAlbumArtist(id);
        if (artist == null)
            return null;

        foreach (var album in artist.Albums)
        {
            album.Album.Source = Source;
            foreach (var track in album.AlbumTracks)
            {
                track.Track.Source = Source;
            }
        }
        return artist;
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        return await _baseLibrary.GetAlbumArtists();
    }

    public async Task<ICollection<Track>> GetAllTracks()
    {
        var tracks = await _baseLibrary.GetAllTracks();
        foreach (var track in tracks)
        {
            track.Source = Source;
        }
        return tracks;
    }

    public async Task<PlayingTrack> GetTrack(string id)
    {
        var track = await _baseLibrary.GetTrack(id);
        track.Source = Source;
        return track;
    }

    public async Task<FullTrack> GetFullTrack(string id)
    {
        var track = await _baseLibrary.GetFullTrack(id);
        track.Source = Source;
        return track;
    }
}