using System.Collections.ObjectModel;

namespace Cadenza.State.Actions.Reducers;

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
    public static ViewAlbumState ReduceFetchViewAlbumResult(ViewAlbumState state, TrackRemovedAction action)
    {
        if (!state.Discs.Any(d => d.Tracks.Any(t => t.TrackId == action.TrackId)))
            return state;

        var discs = state.Discs.ToList();
        var updatedDiscs = new List<DiscVM>();

        foreach (var disc in discs)
        {
            var tracks = disc.Tracks.ToList();
            var removedTrack = tracks.SingleOrDefault(t => t.TrackId == action.TrackId);

            if (removedTrack != null)
            {
                tracks.Remove(removedTrack);
                updatedDiscs.Add(disc with { Tracks = tracks });
            }
            else
            {
                updatedDiscs.Add(disc);
            }
        }

        return state with
        {
            Discs = new ReadOnlyCollection<DiscVM>(updatedDiscs)
        };
    }
}
