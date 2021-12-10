namespace Cadenza.Library;

public abstract class SourceLibrary : ILibrary
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

        artist.Artist.AddSourceId(Source, id);
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
        var artists = await _baseLibrary.GetAlbumArtists();
        foreach (var artist in artists)
        {
            artist.AddSourceId(Source, artist.Id);
        }
        return artists;
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

    public async Task<TrackFull> GetTrack(string id)
    {
        var track = await _baseLibrary.GetTrack(id);
        track.Track.Source = Source;
        track.Album.Source = Source;
        track.Artist.AddSourceId(Source, track.Artist.Id);
        return track;
    }
}