using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class EditArtistBase : FormBase<ArtistInfo>
{
    [Inject]
    public IUpdateService Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesService { get; set; }

    public ArtistUpdate Update { get; set; }

    public ArtistInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdate(Model);
    }

    protected async Task OnSubmit()
    {
        try
        {
            Update.ConfirmUpdates();

            if (!Update.Updates.Any())
            {
                Cancel();
                return;
            }

            await Repository.UpdateArtist(Update);
            Alert.Success("Artist updated");
            await UpdatesService.UpdateArtist(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating artist: " + ex.Message);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
