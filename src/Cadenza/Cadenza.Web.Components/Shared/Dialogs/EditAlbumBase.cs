using Cadenza.Common.Domain.Model;
using Cadenza.Common.Interfaces.Utilities;
using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class EditAlbumBase : FormBase<AlbumInfo>
{
    private const string ArtworkSearchUrl = "https://www.google.com/search?tbm=isch&q=%22{0}%22+%22{1}%22";

    [Inject]
    public IImageConverter ImageConverter { get; set; }

    [Inject]
    public IHttpHelper HttpHelper { get; set; } 

    [Inject]
    public INavigation Navigation { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdateService UpdateService { get; set; }

    [Inject]
    public IUpdatesCoordinator UpdatesCoordinator { get; set; }

    public string ArtworkUrl { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
    }

    protected Task OnLoad()
    {
        return Task.CompletedTask;
    }

    protected async Task OnSearch()
    {
        var searchUrl = GetSearchUrl();
        await Navigation.OpenNewTab(searchUrl);
    }

    protected async Task OnSubmit()
    {
        try
        {
            Update.ConfirmUpdates();

            if (!Update.Updates.Any())
            {
                Cancel();
                return;
            }

            await UpdateService.UpdateAlbum(Update);
            Alert.Success("Album updated");
            await UpdatesCoordinator.UpdateAlbum(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating album: " + ex.Message);
            Alert.Error("Error updating album: " + ex.StackTrace);
        }
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(ArtworkSearchUrl, artist, title);
    }

    protected async Task OnUpdateUrl()
    {
        if (string.IsNullOrWhiteSpace(ArtworkUrl))
        {
            Alert.Error("No URL entered");
            return;
        }

        if (!Uri.TryCreate(ArtworkUrl, UriKind.Absolute, out Uri uri))
        {
            Alert.Error("URL is invalid");
            return;
        }

        try
        {
            var image = await GetArtwork(uri);
            EditableItem.ArtworkUrl = ImageConverter.GetBase64UrlFromImage(image);
        }
        catch (Exception ex)
        {
            Alert.Error(ex.Message);
            return;
        }    
    }

    private async Task<ArtworkImage> GetArtwork(Uri uri)
    {
        try
        {
            var response = await HttpHelper.Get(uri.ToString());

            if (!response.IsSuccessStatusCode)
            {
                // Log exact error
                throw new Exception("Access was not allowed");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var mimeType = response.Content.Headers.ContentType.MediaType;

            if (!mimeType.StartsWith("image/"))
            {
                throw new Exception("Not an image URL");
            }

            return new ArtworkImage(bytes, mimeType);

        }
        catch (Exception ex)
        {
            // Log exact error
            throw new Exception("Fetching image failed");
        }
    }
}
