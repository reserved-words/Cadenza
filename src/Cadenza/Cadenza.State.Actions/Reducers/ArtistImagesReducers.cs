using System.Collections.ObjectModel;

namespace Cadenza.State.Actions.Reducers;

public static class ArtistImagesReducers
{
    [ReducerMethod]
    public static ArtistImagesState ReduceArtistImageResultAction(ArtistImagesState state, FetchArtistImageResultAction action) 
    {
        var images = new Dictionary<int, string>(state.Images);
        if (!images.ContainsKey(action.ArtistId))
        {
            images.Add(action.ArtistId, action.Result);
        }
        else
        {
            images[action.ArtistId] = action.Result;
        }
        return state with
        {
            Images = new ReadOnlyDictionary<int, string>(images)
        };
    }
}
