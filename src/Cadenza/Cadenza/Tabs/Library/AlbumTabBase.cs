using System.Reflection;

namespace Cadenza.Tabs.Library;

public class AlbumTabBase : ComponentBase
{
    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IAlbumRepository Repository { get; set; }

    [Parameter]
    public string Id { get; set; }

    public AlbumInfo Album { get; set; }

    public List<Disc> Discs { get; set; } = new();

    private Guid _trackRemovedSubscriptionId = Guid.Empty;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<TrackRemovedEventArgs>(OnTrackRemoved, out _trackRemovedSubscriptionId);
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateAlbum();
    }

    private Task OnTrackRemoved(object sender, TrackRemovedEventArgs args)
    {
        var disc = Discs.SingleOrDefault(d => d.Tracks.Any(t => t.TrackId == args.TrackId));
        if (disc == null)
            return Task.CompletedTask;

        var trackOnDisc = disc.Tracks.SingleOrDefault(t => t.TrackId == args.TrackId);
        if (trackOnDisc == null)
            return Task.CompletedTask;

        disc.Tracks.Remove(trackOnDisc);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task UpdateAlbum()
    {
        Album = await Repository.GetAlbum(Id);

        var tracks = await Repository.GetAlbumTracks(Id);

        Discs = tracks.GroupByDisc();

        StateHasChanged();
    }

    public void Dispose()
    {
        if (_trackRemovedSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<TrackRemovedEventArgs>(_trackRemovedSubscriptionId);
        }
    }
}
