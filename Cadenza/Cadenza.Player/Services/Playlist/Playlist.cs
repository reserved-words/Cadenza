using Cadenza.Database;

namespace Cadenza.Player;

public class Playlist : IPlaylist
{
    private Stack<PlayTrack> _played;
    private PlayTrack _playing;
    private Stack<PlayTrack> _toPlay;
    private List<PlayTrack> _allTracks;

    private readonly PlaylistDefinition _definition;

    public PlayTrack Current => _playing;

    public Playlist(PlaylistDefinition def)
    {
        _definition = def;

        _allTracks = def.Tracks;

        var firstIndex = def.First == null
            ? 0
            : _allTracks.IndexOf(def.First);

        _played = new Stack<PlayTrack>(_allTracks.Take(firstIndex));
        _playing = _allTracks[firstIndex];
        //_playing.IsCurrent = true;
        _toPlay = new Stack<PlayTrack>(_allTracks.Skip(firstIndex + 1).Reverse());
    }

    //public PlayingTrack Current { get; private set; }
    
    public bool CurrentIsFirst => _played.Count == 0;
    public bool CurrentIsLast => _toPlay.Count == 0;

    public PlaylistType Type => _definition.Type;
    public string Name => _definition.Name;
    public List<PlayTrack> Tracks => _definition.Tracks;

    public async Task<PlayTrack> MoveNext()
    {
        //_playing.IsCurrent = false;
        _played.Push(_playing);
        _playing = _toPlay.Count == 0
            ? null
            : _toPlay.Pop();
        //_playing.IsCurrent = true;

       // Current = await GetCurrent();

        return _playing;
    }

    public async Task<PlayTrack> MovePrevious()
    {
        if (_played.Count > 0)
        {
            if (_playing != null)
            {
                //_playing.IsCurrent = false;
                _toPlay.Push(_playing);
            }
            _playing = _played.Pop();
        }

        //Current = await GetCurrent();

        //_playing.IsCurrent = true;
        return _playing;
    }

    //private async Task<PlayingTrack> GetCurrent()
    //{
    //    return await _trackRepository.GetSummary(_playing.Source, _playing.Id);
    //}
}