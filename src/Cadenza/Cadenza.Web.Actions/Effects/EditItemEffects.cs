namespace Cadenza.Web.Actions.Effects;

public class EditItemEffects
{
    [EffectMethod]
    public Task HandleFetchEditItemRequest(ViewEditItemRequest action, IDispatcher dispatcher)
    {
        object fetchRequest = action.Type switch
        {
            LibraryItemType.Album => new FetchEditAlbumRequest(action.Id),
            LibraryItemType.Artist => new FetchEditArtistRequest(action.Id),
            LibraryItemType.Track => new FetchEditTrackRequest(action.Id),
            _ => throw new NotImplementedException()
        };

        dispatcher.Dispatch(fetchRequest);
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleCancelEditItemRequest(CancelEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleRemoveEditItemRequest(RemoveEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest("Remove item not implemented yet", null, null));
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;

    }

    [EffectMethod]
    public Task HandleSaveEditItemRequest(SaveEditItemRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NotificationErrorRequest("Save item not implemented yet", null, null));
        dispatcher.Dispatch(new ViewEditEndRequest());
        return Task.CompletedTask;
    }
}
