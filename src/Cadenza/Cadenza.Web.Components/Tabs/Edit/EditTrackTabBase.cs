﻿
namespace Cadenza.Web.Components.Tabs.Edit;

public class EditTrackTabBase : FluxorComponent
{
    [Inject] public IState<EditTrackState> EditTrackState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IEditItemMapper Mapper { get; set; }

    public bool Loading => EditTrackState.Value.IsLoading;
    public TrackDetailsVM Track => EditTrackState.Value.Track;

    protected EditableTrack EditableTrack { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditTrackResult>(OnEditTrackFetched);
        SubscribeToAction<SaveEditItemRequest>(OnSave);
        base.OnInitialized();
    }

    private void OnSave(SaveEditItemRequest request)
    {
        if (Track == null)
            return;

        Dispatcher.Dispatch(new NotificationErrorRequest("Save track not implemented yet", null, null));
    }

    private void OnEditTrackFetched(FetchEditTrackResult result)
    {
        if (Track == null)
            return;

        EditableTrack = Mapper.MapTrack(Track);
    }
}
