namespace Cadenza.Web.Components.Player;

public class CurrentTrackArtworkBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public TrackFullVM Model { get; set; }

    public string AlbumDisplay { get; private set; }
    public string ArtworkUrl { get; private set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchAlbumArtworkResultAction>(OnAlbumArtworkFetched);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        AlbumDisplay = Model == null
            ? null
            : $"{Model.Album.Title} ({Model.Album.ArtistName})";

        Dispatcher.Dispatch(new FetchAlbumArtworkRequest(Model?.Album.Id ?? 0, Model?.Album.ArtworkBase64));
    }

    private void OnAlbumArtworkFetched(FetchAlbumArtworkResultAction action)
    {
        if (Model == null)
        {
            if (action.AlbumId == 0)
            {
                ArtworkUrl = action.Result;
            }
        }
        else if (action.AlbumId == Model.Album.Id)
        {
            ArtworkUrl = action.Result;
        }
    }

    protected void OnViewAlbum()
    {
        Dispatcher.Dispatch(new ViewItemRequest(PlayerItemType.Album, Model.Album.Id.ToString(), Model.Album.Title));
    }
}
