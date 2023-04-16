namespace Cadenza.Web.Core.Interfaces;

internal interface IPlaylist
{
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    void RemoveTrack(int trackId);

    PlaylistId Id { get; }
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}