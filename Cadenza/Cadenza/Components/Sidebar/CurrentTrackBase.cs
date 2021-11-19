namespace Cadenza;

public class CurrentTrackBase : ComponentBase
{
    [Inject]
    public IViewModelLibrary Library { get; set; }

    [Inject]
    public ITrackProgressedConsumer TrackProgressConsumer { get; set; }

    [Inject]
    public IFavouritesController FavouritesController { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IFavouritesConsumer Favourites { get; set; }

    public double Progress { get; set; }

    public TrackSummary Model { get; set; }

    public bool IsFavourite { get; set; }

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        Library.AlbumUpdated += OnAlbumUpdated;
        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    private async Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs e)
    {
        if (Model == null || Model.Album.Id != e.Update.Id)
            return;

        Model.Album.ReleaseType = e.Update.ReleaseType;
        Model.Album.Year = e.Update.Year;
        StateHasChanged();
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        IsFavourite = false;
        StateHasChanged();
        Model = await Library.GetTrackSummary(e.CurrentTrack.Model.Source, e.CurrentTrack.Model.Id);
        IsFavourite = await Favourites.IsFavourite(Model);
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }

    public async Task Favourite()
    {
        await FavouritesController.Favourite(Model);
        IsFavourite = true;
        StateHasChanged();
    }

    public async Task Unfavourite()
    {
        await FavouritesController.Unfavourite(Model);
        IsFavourite = false;
        StateHasChanged();
    }
}
