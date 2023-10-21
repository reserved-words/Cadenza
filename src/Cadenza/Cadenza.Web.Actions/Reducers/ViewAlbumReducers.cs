namespace Cadenza.Web.Actions.Reducers;

public static class ViewAlbumReducers
{
    [ReducerMethod(typeof(FetchViewAlbumRequest))]
    public static ViewAlbumState ReduceFetchViewAlbumAction(ViewAlbumState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceFetchViewAlbumResult(ViewAlbumState state, FetchViewAlbumResult action) => state with
    {
        IsLoading = false,
        Album = action.Album,
        Discs = action.Discs
    };

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumTracksUpdatedAction(ViewAlbumState state, AlbumTracksUpdatedAction action)
    {
        if (state.Album.Id != action.AlbumId)
            return state;

        var discs = new Dictionary<int, List<AlbumTrackVM>>();

        foreach (var track in action.UpdatedTracks)
        {
            if (!discs.ContainsKey(track.DiscNo))
            {
                discs.Add(track.DiscNo, new List<AlbumTrackVM>());
            }

            discs[track.DiscNo].Add(track);
        }

        var updatedDiscs = discs.Select(d => new DiscVM(d.Key, new ReadOnlyCollection<AlbumTrackVM>(d.Value))).ToList();

        return state with
        {
            Discs = new ReadOnlyCollection<DiscVM>(updatedDiscs)
        };
    }

    [ReducerMethod]
    public static ViewAlbumState ReduceAlbumUpdatedAction(ViewAlbumState state, AlbumUpdatedAction action)
    {
        if (state.Album == null || state.Album.Id != action.UpdatedAlbum.Id)
            return state;

        return state with { Album = action.UpdatedAlbum };
    }
}
