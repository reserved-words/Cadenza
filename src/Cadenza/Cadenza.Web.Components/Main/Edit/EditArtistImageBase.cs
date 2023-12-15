namespace Cadenza.Web.Components.Main.Edit;

public class EditArtistImageBase : FluxorComponent
{
    [Inject] public IImageFinder ImageFinder { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public EditableArtist Model { get; set; }

    protected string ImageUrl { get; set; }
    protected string OriginalImageSrc { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchArtistImageResultAction>(OnArtistImageFetched);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        if (Model == null)
            return;

        Dispatcher.Dispatch(new FetchArtistImageRequest(Model.Id, Model.ImageBase64));
    }

    private void OnArtistImageFetched(FetchArtistImageResultAction action)
    {
        if (action.ArtistId != Model.Id)
            return;

        OriginalImageSrc = action.Result;
    }

    protected void OnSearch(SearchSource source)
    {
        var searchUrl = ImageFinder.GetArtistImageSearchUrl(Model.Name, source);
        Dispatcher.Dispatch(new NavigationRequest(searchUrl, true));
    }

    protected async Task OnUpdateUrl()
    {
        if (string.IsNullOrWhiteSpace(ImageUrl))
            return;

        try
        {
            Model.ImageBase64 = await ImageFinder.GetBase64ArtworkSource(ImageUrl);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Dispatcher.Dispatch(new NotificationErrorRequest("Error loading image", ex.Message, ex.StackTrace));
            return;
        }
    }
}
