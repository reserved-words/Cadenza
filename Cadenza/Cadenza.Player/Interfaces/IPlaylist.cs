namespace Cadenza.Player;

public interface IPlaylist
{
    PlaylistType Type { get; }
    string Name { get; }
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}