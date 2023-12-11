using Cadenza.Common.Enums.Extensions;
using Cadenza.Web.Common.Extensions;

namespace Cadenza.Web.Actions.Reducers;

public static class ViewArtistReducers
{
    [ReducerMethod(typeof(FetchViewArtistRequest))]
    public static ViewArtistState ReduceFetchViewArtistAction(ViewArtistState state) => state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static ViewArtistState ReduceFetchViewArtistResult(ViewArtistState state, FetchViewArtistResult action) => state with
    {
        IsLoading = false,
        Artist = action.Artist,
        Releases = action.Releases
    };

    [ReducerMethod]
    public static ViewArtistState ReduceArtistReleasesUpdatedAction(ViewArtistState state, ArtistReleasesUpdatedAction action)
    {
        if (state.Artist.Id != action.ArtistId)
            return state;

        var releases = new List<AlbumVM>();

        foreach (var releaseGroup in state.Releases)
        {
            foreach (var album in releaseGroup.Albums)
            {
                var updatedAlbum = action.UpdatedArtistReleases.SingleOrDefault(t => t.Id == album.Id) ?? album;
                releases.Add(updatedAlbum);
            }
        }

        return state with
        {
            Releases = releases.GroupByReleaseType()
        };
    }

    [ReducerMethod]
    public static ViewArtistState ReduceArtistUpdatedAction(ViewArtistState state, ArtistUpdatedAction action)
    {
        if (state.Artist == null || state.Artist.Id != action.UpdatedArtist.Id)
            return state;

        return state with { Artist = action.UpdatedArtist };
    }
}
