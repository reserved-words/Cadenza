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
        Messenger.Subscribe<TrackUpdatedEventArgs>(OnTrackUpdated);
    }

    private Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs args)
    {
        if (Model == null || args.Update.Id != Model.Album.Id)
            return Task.CompletedTask;

        args.Update.ApplyUpdates(Model.Album);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnTrackUpdated(object sender, TrackUpdatedEventArgs args)
    {
        if (Model == null || args.Update.Id != Model.Id)
            return Task.CompletedTask;

        args.Update.ApplyUpdates(Model.Track);
        StateHasChanged();
        return Task.CompletedTask;
    }
}
