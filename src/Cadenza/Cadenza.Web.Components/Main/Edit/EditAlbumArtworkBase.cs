namespace Cadenza.Web.Components.Main.Edit;

public class EditAlbumArtworkBase : FluxorComponent
{
    [Inject] public IImageFinder ImageFinder { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public EditableAlbum Model { get; set; }

    protected string ArtworkUrl { get; set; }
    protected string OriginalArtworkSrc { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchAlbumArtworkResultAction>(OnAlbumArtworkFetched);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        if (Model == null)
            return;

        Dispatcher.Dispatch(new FetchAlbumArtworkRequest(Model.Id, Model.ArtworkBase64));
    }

    private void OnAlbumArtworkFetched(FetchAlbumArtworkResultAction action)
    {
        if (action.AlbumId != Model.Id)
            return;

        OriginalArtworkSrc = action.Result;
    }

    protected async Task OnLoad()
    {
        try
        {
            var artworkUrl = await ImageFinder.GetAlbumArtworkUrl(Model.ArtistName, Model.Title);

            if (string.IsNullOrWhiteSpace(artworkUrl))
            {
                throw new Exception("No artwork found");
            }

            ArtworkUrl = artworkUrl;

            await OnUpdateUrl();
        }
        catch (Exception ex)
        {
            Dispatcher.Dispatch(new NotificationErrorRequest("Error loading artwork", ex.Message, ex.StackTrace));
        }
    }

    protected void OnSearch()
    {
        var searchUrl = ImageFinder.GetAlbumArtworkSearchUrl(Model.ArtistName, Model.Title);
        Dispatcher.Dispatch(new NavigationRequest(searchUrl, true));
    }

    protected async Task OnUpdateUrl()
    {
        if (string.IsNullOrWhiteSpace(ArtworkUrl))
            return;

        try
        {
            Model.ArtworkBase64 = await ImageFinder.GetBase64ArtworkSource(ArtworkUrl);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Dispatcher.Dispatch(new NotificationErrorRequest("Error loading image", ex.Message, ex.StackTrace));
            return;
        }
    }
}
