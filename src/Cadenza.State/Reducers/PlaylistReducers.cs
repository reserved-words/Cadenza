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
    public static PlaylistState ReducePlaylistStopRequest(PlaylistState state) => PlaylistState.Init();
}
