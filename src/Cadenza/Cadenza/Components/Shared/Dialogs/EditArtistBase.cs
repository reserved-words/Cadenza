using Cadenza.Update.LibraryUpdaters;

namespace Cadenza;

public class EditArtistBase : FormBase<ArtistInfo>
{
    [Inject]
    public IMergedArtistUpdater Updater { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    public ArtistUpdate Update { get; set; }

    protected override void OnParametersSet()
    {
        Update = new ArtistUpdate(Model);
    }

    protected async Task OnSubmit()
    {
        try
        {
            await Updater.UpdateArtist(Update);
            Alert.Success("Artist updated");
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating artist");
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
