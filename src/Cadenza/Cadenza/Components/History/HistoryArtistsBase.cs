﻿using Fluxor;

namespace Cadenza.Components.History;

public class HistoryArtistsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<PlayHistoryArtistsState> PlayHistoryArtistsState { get; set; }

    protected List<PlayedArtist> Items => PlayHistoryArtistsState.Value.Items;
    protected bool IsLoading => PlayHistoryArtistsState.Value.IsLoading;
    protected HistoryPeriod Period => PlayHistoryArtistsState.Value.Period;

    protected void UpdateItems(HistoryPeriod period)
    {
        Dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(period));
    }
}
