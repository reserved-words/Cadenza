namespace Cadenza.Core;

public interface IPlaylist
{
    PlaylistType Type { get; }
    string Name { get; }
    bool MixedSource { get; }
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}