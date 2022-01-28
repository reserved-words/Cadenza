namespace Cadenza.Core;

public interface IPlaylist
{
    PlaylistType Type { get; }
    string Name { get; }
    bool MixedSource { get; }
    Task<BasicTrack> MoveNext();
    Task<BasicTrack> MovePrevious();
    BasicTrack Current { get; }
    bool CurrentIsLast { get; }
}