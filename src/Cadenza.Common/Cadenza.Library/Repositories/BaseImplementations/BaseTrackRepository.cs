namespace Cadenza.Library;

public class BaseTrackRepository : IBaseTrackRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, TrackInfo> _tracks;
    private Dictionary<string, AlbumInfo> _albums;
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, AlbumTrackLink> _albumTracks;

    public BaseTrackRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        if (!_tracks.ContainsKey(id))
            return null;

        var track = _tracks[id];
        var album = _albums[track.AlbumId];
        var artist = _artists[track.ArtistId];
        var albumTrack = _albumTracks[track.Id];
        var albumArtist = _artists[album.ArtistId];

        return new TrackFull
        {
            Track = track,
            Artist = artist,
            Album = album,
            AlbumTrack = albumTrack,
            AlbumArtist = albumArtist
        };
    }

    public async Task Populate()
    {
        var library = await _library.Get();
        _tracks = library.Tracks.ToDictionary(a => a.Id, a => a);
        _albums = library.Albums.ToDictionary(a => a.Id, a => a);
        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _albumTracks = library.AlbumTrackLinks.ToDictionary(a => a.TrackId, a => a);
    }
}
