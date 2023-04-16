namespace Cadenza.Web.Core.Services;

internal class Playlist : IPlaylist
{
    private Stack<PlayTrack> _played;
    private Stack<PlayTrack> _toPlay;
    private readonly List<PlayTrack> _allTracks;

    private PlayTrack _playing;

    public PlayTrack Current => _playing;

    public Playlist(PlaylistDefinition def)
    {
        Id = def.Id;
        _allTracks = def.Tracks.ToList();

        PopulateToPlay(_allTracks);
        PopulatePlayed();

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

    public void RemoveTrack(int trackId)
    {
        var track = _allTracks.SingleOrDefault(t => t.Id == trackId);
        if (track == null)
            return;

        _allTracks.Remove(track);

        if (_played.Contains(track))
        {
            var playedTracks = new List<PlayTrack>(_played);
            playedTracks.Remove(track);
            PopulatePlayed(playedTracks);
        }

        if (_toPlay.Contains(track))
        {
            var toPlayTracks = new List<PlayTrack>(_toPlay);
            toPlayTracks.Remove(track);
            PopulateToPlay(toPlayTracks);
        }
    }

    private void PopulatePlayed(List<PlayTrack> played = null)
    {
        _played = played == null 
            ? new Stack<PlayTrack>()
            : new Stack<PlayTrack>(played);
    }

    private void PopulateToPlay(List<PlayTrack> toPlay)
    {
        toPlay.Reverse();
        _toPlay = new Stack<PlayTrack>(toPlay);
    }
}