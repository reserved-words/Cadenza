using Cadenza.Core.App;

namespace Cadenza;

public class MenuAlbumBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string Style { get; set; }

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string Id { get; set; }

    [Parameter]
    public LibrarySource Source { get; set; }

    public Task OnEdit()
    {
        return Task.CompletedTask;

        //var albumUpdate = new AlbumUpdate(Album);

        //var (saved, data) = await DialogService.DisplayForm<EditAlbum, AlbumUpdate>(albumUpdate, "Edit Album");

        //if (!saved || !data.IsUpdated)
        //    return;

        //var success = await LibraryController.UpdateAlbum(data);

        //if (success)
        //{
        //    Alert.Success("Album updated");
        //}
        //else
        //{
        //    Alert.Error("Error updating album");
        //}
    }

    public async Task OnPlay()
    {
        await Player.PlayAlbum(Id);
    }
}