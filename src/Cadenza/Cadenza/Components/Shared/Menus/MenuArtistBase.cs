using Cadenza.Components.Shared.Dialogs;
using Cadenza.Core.App;
using Cadenza.Core.Updates;
using Cadenza.Library;

namespace Cadenza;

public class MenuArtistBase : ComponentBase
{
    [Inject]
    public IMergedArtistRepository Repository { get; set; }

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

    public async Task OnEdit()
    {
        var artist = await Repository.GetArtist(Id);

        var artistUpdate = new ArtistUpdate(artist);

        var (saved, data) = await DialogService.DisplayForm<EditArtist, ArtistUpdate>(artistUpdate, "Edit Artist");

        if (!saved)
            return;

        var success = true;

        // var success = await Library.UpdateArtist(data);

        if (success)
        {
            Alert.Success("Artist updated");
        }
        else
        {
            Alert.Error("Error updating artist");
        }
    }

    public async Task OnPlay()
    {
        await Player.PlayArtist(Id);
    }
}
