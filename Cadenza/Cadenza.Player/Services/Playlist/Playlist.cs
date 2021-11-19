namespace Cadenza.Player;

public class Playlist : IPlaylist
{
    private Stack<PlaylistTrackViewModel> _played;
    private PlaylistTrackViewModel _playing;
    private Stack<PlaylistTrackViewModel> _toPlay;
    private List<PlaylistTrackViewModel> _allTracks;

    private readonly PlaylistDefinition _definition;

    public Playlist(PlaylistDefinition def)
    {
        _definition = def;

        _allTracks = def.Sections.All;

        var firstIndex = def.First == null
            ? 0
            : _allTracks.IndexOf(def.First);

        _played = new Stack<PlaylistTrackViewModel>(_allTracks.Take(firstIndex));
        _playing = _allTracks[firstIndex];
        _playing.IsCurrent = true;
        _toPlay = new Stack<PlaylistTrackViewModel>(_allTracks.Skip(firstIndex + 1).Reverse());
    }

    public PlaylistTrackViewModel Current => _playing;
    public bool CurrentIsFirst => _played.Count == 0;
    public bool CurrentIsLast => _toPlay.Count == 0;

    public PlaylistType Type => _definition.Type;
    public string Name => _definition.Name;
    public SplitList<PlaylistTrackViewModel> Sections => _definition.Sections;

    public PlaylistTrackViewModel MoveNext()
    {
        _playing.IsCurrent = false;
        _played.Push(_playing);
        _playing = _toPlay.Count == 0
            ? null
            : _toPlay.Pop();
        _playing.IsCurrent = true;
        return _playing;
    }

    public PlaylistTrackViewModel MovePrevious()
    {
        if (_played.Count > 0)
        {
            if (_playing != null)
            {
                _playing.IsCurrent = false;
                _toPlay.Push(_playing);
            }
            _playing = _played.Pop();
        }

        _playing.IsCurrent = true;
        return _playing;
    }
}