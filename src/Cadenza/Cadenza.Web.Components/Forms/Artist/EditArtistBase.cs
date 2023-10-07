namespace Cadenza.Web.Components.Forms.Artist;

public class EditArtistBase : FormBase<ArtistDetailsVM>
{
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public ArtistUpdateVM Update { get; set; }
    public List<GroupingVM> Groupings => GroupingsState.Value.Groupings;
    public ArtistDetailsVM EditableItem => Update.UpdatedItem;

    protected override async Task OnInitializedAsync()
    {
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        await base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdateVM(Model);
    }

    protected void OnSubmit()
    {
        Update.ConfirmUpdates();

        if (!Update.Updates.Any())
        {
            Cancel();
            return;
        }

        Dispatcher.Dispatch(new ArtistUpdateRequest(Model.Id, Update));
    }

    private void OnArtistUpdated(ArtistUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
