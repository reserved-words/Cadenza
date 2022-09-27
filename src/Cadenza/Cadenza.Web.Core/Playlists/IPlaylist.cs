using Cadenza.Domain.Model;

namespace Cadenza.Web.Core.Playlists;

public interface IPlaylist
{
    PlaylistId Id { get; }
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}