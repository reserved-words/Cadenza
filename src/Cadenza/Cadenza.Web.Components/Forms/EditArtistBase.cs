using Cadenza.Common.Domain.Model.Library;
using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Components.Forms;

public class EditArtistBase : FormBase<ArtistInfo>
{
    [Inject] public IAdminRepository AdminRepository { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public ArtistUpdate Update { get; set; }
    public List<Grouping> Groupings { get; set; } = new List<Grouping>();
    public ArtistInfo EditableItem => Update.UpdatedItem;

    protected override async Task OnInitializedAsync()
    {
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        Groupings = await AdminRepository.GetGroupingOptions();
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
