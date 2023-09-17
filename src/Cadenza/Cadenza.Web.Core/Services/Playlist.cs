//namespace Cadenza.Web.Core.Services;

//internal class Playlist : IPlaylist
//{
//    private Stack<PlayTrack> _played;
//    private Stack<PlayTrack> _toPlay;

//    private PlayTrack _playing;

//    public PlayTrack Current => _playing;

//    public Playlist(PlaylistDefinition def)
//    {
//        Id = def.Id;
//        var allTracks = def.Tracks.ToList();

//        PopulateToPlay(allTracks);
//        PopulatePlayed();

//        _playing = _toPlay.Pop();
//    }

//    public bool CurrentIsLast => _toPlay.Count == 0;

//    public PlaylistId Id { get; }

//    public PlayTrack MoveNext()
//    {
//        _played.Push(_playing);

//        _playing = _toPlay.Count == 0
//            ? null
//            : _toPlay.Pop();

//        return _playing;
//    }

//    public PlayTrack MovePrevious()
//    {
//        if (_played.Count > 0)
//        {
//            if (_playing != null)
//            {
//                _toPlay.Push(_playing);
//            }
//            _playing = _played.Pop();
//        }

//        return _playing;
//    }

//    public void RemoveTrack(int trackId)
//    {
//        var playedTracks = new List<PlayTrack>(_played);
//        var playedTrack = playedTracks.FirstOrDefault(t => t.Id == trackId);

//        if (playedTrack != null)
//        {
//            playedTracks.Remove(playedTrack);
//            PopulatePlayed(playedTracks);
//        }

//        var toPlayTracks = new List<PlayTrack>(_toPlay);
//        var toPlayTrack = toPlayTracks.FirstOrDefault(t => t.Id == trackId);

//        if (toPlayTrack != null)
//        {
//            toPlayTracks.Remove(toPlayTrack);
//            PopulateToPlay(toPlayTracks);
//        }
//    }

//    //private void PopulatePlayed(List<PlayTrack> played = null)
//    //{
//    //    _played = played == null 
//    //        ? new Stack<PlayTrack>()
//    //        : new Stack<PlayTrack>(played);
//    //}

//    //private void PopulateToPlay(List<PlayTrack> toPlay)
//    //{
//    //    toPlay.Reverse();
//    //    _toPlay = new Stack<PlayTrack>(toPlay);
//    //}
//}