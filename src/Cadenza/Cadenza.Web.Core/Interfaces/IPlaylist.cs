using Cadenza.Common.Domain.Model;

namespace Cadenza.Web.Core.Interfaces;

internal interface IPlaylist
{
    PlaylistId Id { get; }
    Task<PlayTrack> MoveNext();
    Task<PlayTrack> MovePrevious();
    PlayTrack Current { get; }
    bool CurrentIsLast { get; }
}