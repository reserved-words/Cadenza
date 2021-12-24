namespace Cadenza;

public class MenuTrackBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public ILibraryController Library { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string TrackId { get; set; }

    public async Task OnEdit()
    {
        //var update = new TrackUpdate(Track);

        //var (saved, data) = await DialogService.DisplayForm<EditTrack, TrackUpdate>(update, "Edit Track");

        //if (!saved)
        //    return;

        //var success = await Library.UpdateTrack(data);

        //if (success)
        //{
        //    Alert.Success("Track updated");
        //}
        //else
        //{
        //    Alert.Error("Error updating track");
        //}
    }
}
