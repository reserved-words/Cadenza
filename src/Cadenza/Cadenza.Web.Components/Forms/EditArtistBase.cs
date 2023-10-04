using Fluxor;

namespace Cadenza.Web.Components.Forms;

public class EditArtistBase : FormBase<ArtistDetails>
{
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public ArtistUpdate Update { get; set; }
    public List<Grouping> Groupings => GroupingsState.Value.Groupings;
    public ArtistDetails EditableItem => Update.UpdatedItem;

    protected override async Task OnInitializedAsync()
    {
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        await base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdate(Model);
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
