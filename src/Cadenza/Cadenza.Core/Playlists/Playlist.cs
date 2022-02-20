namespace Cadenza.Core;

public class Playlist : IPlaylist
{
    private Stack<PlayTrack> _played;
    private PlayTrack _playing;
    private Stack<PlayTrack> _toPlay;
    private List<PlayTrack> _allTracks;

    public PlayTrack Current => _playing;

    public Playlist(PlaylistDefinition def)
    {
        Id = def.Id;

        _allTracks = def.Tracks.ToList();

        _played = new Stack<PlayTrack>(_allTracks);
        _playing = _allTracks[0];
        _toPlay = new Stack<PlayTrack>(_allTracks);
    }

    public bool CurrentIsLast => _toPlay.Count == 0;

    public PlaylistId Id { get; }

    public async Task<PlayTrack> MoveNext()
    {
        _played.Push(_playing);

        _playing = _toPlay.Count == 0
            ? null
            : _toPlay.Pop();

        return _playing;
    }

    public async Task<PlayTrack> MovePrevious()
    {
        if (_played.Count > 0)
        {
            if (_playing != null)
            {
                _toPlay.Push(_playing);
            }
            _playing = _played.Pop();
        }

        return _playing;
    }
}