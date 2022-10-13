using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    [Inject]
    public IUpdateService Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesService { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
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

            await Repository.UpdateAlbum(Update);
            Alert.Success("Album updated");
            await UpdatesService.UpdateAlbum(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating album: " + ex.Message);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
