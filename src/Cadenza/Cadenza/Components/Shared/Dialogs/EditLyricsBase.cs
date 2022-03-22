using Cadenza.Core.App;
using Cadenza.Library;

namespace Cadenza;

public class EditLyricsBase : FormBase<TrackInfo>
{
    [Inject]
    public IMergedTrackRepository Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesController UpdatesService { get; set; }

    public TrackUpdate Update { get; set; }

    public TrackInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new TrackUpdate(Model);
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

            await Repository.UpdateTrack(Update);
            Alert.Success("Lyrics updated");
            await UpdatesService.UpdateLyrics(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating lyrics");
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
