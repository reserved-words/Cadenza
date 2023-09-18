namespace Cadenza.Web.Common.Interfaces;

public interface IPlaylist
{
    int MoveNext();
    int MovePrevious();
    void RemoveTrack(int trackId);

    PlaylistId Id { get; }
    int Current { get; }
    bool CurrentIsLast { get; }
}