namespace Cadenza.Web.Common.Interfaces;

public interface IPlaylist
{
    PlayTrack MoveNext();
    PlayTrack MovePrevious();
    void RemoveTrack(int trackId);

    PlaylistId Id { get; }
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}