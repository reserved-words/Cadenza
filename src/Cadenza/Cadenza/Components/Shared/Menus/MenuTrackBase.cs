using Cadenza.Core.App;
using Cadenza.Interfaces;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Components.Shared.Menus;

public class MenuTrackBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string Id { get; set; }

    [Parameter]
    public LibrarySource Source { get; set; }

    public Task OnEdit()
    {
        return Task.CompletedTask;

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

    public async Task OnPlay()
    {
        await Player.PlayTrack(Id);
    }
}
