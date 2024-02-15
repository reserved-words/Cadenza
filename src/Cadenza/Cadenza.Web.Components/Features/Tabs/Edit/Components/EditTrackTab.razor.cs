namespace Cadenza.Web.Components.Features.Tabs.Edit.Components;

public class EditTrackTabBase : FluxorComponent
{
    [Inject] public IState<EditTrackState> EditTrackState { get; set; }
    [Inject] public IChangeDetector ChangeDetector { get; set; }
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

        var editedTrack = Mapper.MapEditedTrack(EditableTrack);

        if (!ChangeDetector.HasTrackChanged(Track, editedTrack))
        {
            Dispatcher.Dispatch(new NotificationInformationRequest("No changes made"));
            Dispatcher.Dispatch(new CancelEditItemRequest());
        }
        else
        {
            Dispatcher.Dispatch(new TrackUpdateRequest(editedTrack));
        }
    }

    private void OnEditTrackFetched(FetchEditTrackResult result)
    {
        if (Track == null)
            return;

        EditableTrack = Mapper.MapEditableTrack(Track);
    }
}
