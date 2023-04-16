namespace Cadenza.Web.Core.Interfaces;

internal interface IPlaylist
{
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    void RemoveTrack(string trackId);

    PlaylistId Id { get; }
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}