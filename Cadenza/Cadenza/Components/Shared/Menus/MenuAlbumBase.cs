namespace Cadenza;

public class MenuAlbumBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public ILibraryController LibraryController { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string Style { get; set; }

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string AlbumId { get; set; }

    public async Task OnEdit()
    {
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
}