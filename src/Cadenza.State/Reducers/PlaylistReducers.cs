using Cadenza.Common.Domain.Enums;
using Cadenza.State.Actions;
using Cadenza.State.Store;

namespace Cadenza.State.Reducers;

public static class PlaylistReducers
{
    [ReducerMethod]
    public static PlaylistState ReducePlaylistStartRequest(PlaylistState state, PlaylistStartRequest action) => state with
    {
        IsLoading = false,
        Id = action.Definition.Id.Id,
        Type = action.Definition.Id.Type,
        Name = action.Definition.Id.Name
    };

    [ReducerMethod(typeof(PlaylistStopRequest))]
    public static PlaylistState ReducePlaylistStopRequest(PlaylistState state) => state with
    {
        IsLoading = false,
        Id = null,
        Type = PlaylistType.All,
        Name = null
    };
}
