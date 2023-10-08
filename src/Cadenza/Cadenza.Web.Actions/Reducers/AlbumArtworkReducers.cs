using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;
using System.Collections.ObjectModel;

namespace Cadenza.Web.Actions.Reducers;

public static class AlbumArtworkReducers
{
    [ReducerMethod]
    public static AlbumArtworkState ReduceAlbumArtworkResultAction(AlbumArtworkState state, FetchAlbumArtworkResultAction action)
    {
        var images = new Dictionary<int, string>(state.Images);
        if (!images.ContainsKey(action.AlbumId))
        {
            images.Add(action.AlbumId, action.Result);
        }
        else
        {
            images[action.AlbumId] = action.Result;
        }
        return state with
        {
            Images = new ReadOnlyDictionary<int, string>(images)
        };
    }
}
