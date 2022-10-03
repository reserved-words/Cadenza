namespace Cadenza.Web.Core.Services;

internal class Playlist : IPlaylist
{
    private readonly Stack<PlayTrack> _played;
    private readonly Stack<PlayTrack> _toPlay;
    private readonly List<PlayTrack> _allTracks;

    private PlayTrack _playing;

    public PlayTrack Current => _playing;

    public Playlist(PlaylistDefinition def)
    {
        Id = def.Id;
        _allTracks = def.Tracks.ToList();

        var toPlay = new List<PlayTrack>(_allTracks);
        toPlay.Reverse();
        _played = new Stack<PlayTrack>();
        _toPlay = new Stack<PlayTrack>(toPlay);
        _playing = _toPlay.Pop();
    }

    public bool CurrentIsLast => _toPlay.Count == 0;

    public PlaylistId Id { get; }

    public Task<PlayTrack> MoveNext()
    {
        _played.Push(_playing);

        _playing = _toPlay.Count == 0
            ? null
            : _toPlay.Pop();

        return Task.FromResult(_playing);
    }

    public Task<PlayTrack> MovePrevious()
    {
        if (_played.Count > 0)
        {
            if (_playing != null)
            {
                _toPlay.Push(_playing);
            }
            _playing = _played.Pop();
        }

        return Task.FromResult(_playing);
    }
}