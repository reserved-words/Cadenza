using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Services;

internal class PlayCache : IPlayCache
{
    private readonly List<int> _playTracks = new();
    private readonly Dictionary<string, List<int>> _tagPlayTracks = new();

    public void CacheTrack(TrackDetails track, ArtistDetails artist, AlbumDetails album)
    {
         _playTracks.Add(track.Id);
        _tagPlayTracks.Cache(track, artist, album, track.Id);
    }

    public void Clear()
    {
        _playTracks.Clear();
        _tagPlayTracks.Clear();
    }

    public List<int> GetAll()
    {
        return _playTracks;
    }

    public List<int> GetTag(string id)
    {
        return _tagPlayTracks[id];
    }
}
