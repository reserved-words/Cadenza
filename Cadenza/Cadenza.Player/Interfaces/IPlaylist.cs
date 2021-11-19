namespace Cadenza.Player;

public interface IPlaylist
{
    PlaylistType Type { get; }
    string Name { get; }
    PlaylistTrackViewModel Current { get; }
    PlaylistTrackViewModel MoveNext();
    PlaylistTrackViewModel MovePrevious();
    bool CurrentIsFirst { get; }
    bool CurrentIsLast { get; }
    SplitList<PlaylistTrackViewModel> Sections { get; }
}