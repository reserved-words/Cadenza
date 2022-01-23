namespace Cadenza;

public class MenuArtistBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    // [Inject]
    // public ILibraryController Library { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string ArtistId { get; set; }

    public async Task OnEdit()
    {
        //var artistUpdate = new ArtistUpdate(Artist);

        //var (saved, data) = await DialogService.DisplayForm<EditArtist, ArtistUpdate>(artistUpdate, "Edit Artist");

        //if (!saved)
        //    return;

        //var success = await Library.UpdateArtist(data);

        //if (success)
        //{
        //    Alert.Success("Artist updated");
        //}
        //else
        //{
        //    Alert.Error("Error updating artist");
        //}
    }
}
