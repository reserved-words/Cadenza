namespace Cadenza.Library;

internal class DynamicSourceManager
{
    private readonly ILibrary _source;

    private bool _artistsFetched;
    private bool _tracksFetched;

    private List<string> _artistDetailsFetched = new();
    private List<string> _trackDetailsFetched = new();

    public DynamicSourceManager(ILibrary source)
    {
        _source = source;
    }

    public async Task<ArtistFull> GetAlbumArtist(string id)
    {
        if (_artistDetailsFetched.Contains(id))
            return null;

        var artist = await _source.GetAlbumArtist(id);
        _artistDetailsFetched.Add(id);
        return artist;
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        if (_artistsFetched)
            return null;

        var artists = await _source.GetAlbumArtists();
        _artistsFetched = true;
        return artists;
    }

    public async Task<ICollection<Track>> GetAllTracks()
    {
        if (_tracksFetched)
            return null;

        var tracks = await _source.GetAllTracks();
        _tracksFetched = true;
        return tracks;
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        if (_trackDetailsFetched.Contains(id))
            return null;

        var track = await _source.GetTrack(id);
        _trackDetailsFetched.Add(id);
        return track;
    }
}