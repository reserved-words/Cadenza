﻿namespace Cadenza.State.Effects;

public class ViewEffects
{
    [EffectMethod]
    public Task HandleViewItemRequest(ViewItemRequest action, IDispatcher dispatcher)
    {
        FetchRequestedItem(action, dispatcher);
        ClearOtherItems(action, dispatcher);
        return Task.CompletedTask;
    }

    private void FetchRequestedItem(ViewItemRequest action, IDispatcher dispatcher)
    {
        if (action.Type == PlayerItemType.Artist)
        {
            dispatcher.Dispatch(new FetchViewArtistRequest(int.Parse(action.Id)));
        }
        else if (action.Type == PlayerItemType.Album)
        {
            dispatcher.Dispatch(new FetchViewAlbumRequest(int.Parse(action.Id)));
        }
        else if (action.Type == PlayerItemType.Track)
        {
            dispatcher.Dispatch(new FetchViewTrackRequest(int.Parse(action.Id)));
        }
        else if (action.Type == PlayerItemType.Genre)
        {
            dispatcher.Dispatch(new FetchViewGenreRequest(action.Id));
        }
        else if (action.Type == PlayerItemType.Grouping)
        {
            dispatcher.Dispatch(new FetchViewGroupingRequest(new Grouping(int.Parse(action.Id), action.Name)));
        }
    }

    private void ClearOtherItems(ViewItemRequest action, IDispatcher dispatcher)
    {
        if (action.Type != PlayerItemType.Artist)
        {
            dispatcher.Dispatch(new FetchViewArtistResult(null, null));
        }
        if (action.Type != PlayerItemType.Album)
        {
            dispatcher.Dispatch(new FetchViewAlbumResult(null, null));
        }
        if (action.Type != PlayerItemType.Track)
        {
            dispatcher.Dispatch(new FetchViewTrackResult(null));
        }
        if (action.Type != PlayerItemType.Genre)
        {
            dispatcher.Dispatch(new FetchViewGenreResult(null, null));
        }
        if (action.Type != PlayerItemType.Grouping)
        {
            dispatcher.Dispatch(new FetchViewGroupingResult(null, null));
        }
    }
}
