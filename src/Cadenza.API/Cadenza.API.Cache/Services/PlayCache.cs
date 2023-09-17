namespace Cadenza.API.Cache.Services;

internal class PlayCache : IPlayCache
{
    private readonly Dictionary<int, PlayTrack> _playTracks = new();
    private readonly Dictionary<string, List<PlayTrack>> _tagPlayTracks = new();

    public void CacheTrack(TrackInfo track, ArtistInfo artist, AlbumInfo album)
    {
        var playTrack = new PlayTrack
        {
            Id = track.Id,
            IdFromSource = track.IdFromSource,
            Title = track.Title,
            ArtistId = track.ArtistId,
            AlbumId = track.AlbumId,
            Source = track.Source
        };

        _playTracks.Add(playTrack.Id, playTrack);
        _tagPlayTracks.Cache(track, artist, album, playTrack);
    }

    public void Clear()
    {
        _playTracks.Clear();
        _tagPlayTracks.Clear();
    }

    public PlayTrack GetTrack(int id)
    {
        return _playTracks[id];
    }

    public List<PlayTrack> GetAll()
    {
        return _playTracks.Values.ToList();
    }

    public List<PlayTrack> GetTag(string id)
    {
        return _tagPlayTracks[id];
    }
}
