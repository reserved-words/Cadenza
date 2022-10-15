namespace Cadenza.Web.Player.Components;

public class CurrentTrackBase : ComponentBase
{
    [Inject]
    public IMessenger Messenger { get; set; }

    [Parameter]
    public bool Empty { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Parameter]
    public TrackFull Model { get; set; }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<AlbumUpdatedEventArgs>(OnAlbumUpdated);
    }

    private Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs args)
    {
        if (args.Update.Id == Model.Album.Id)
        {
            args.Update.ApplyUpdates(Model.Album);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
