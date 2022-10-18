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

    [Inject]
    public IWebInfoService WebInfoService { get; set; }

    public string ArtworkUrl { get; set; }

    public AlbumUpdate Update { get; set; }

    public AlbumInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new AlbumUpdate(Model);
    }

    protected async Task OnLoad()
    {
        try
        {
            var artworkUrl = await WebInfoService.GetAlbumArtworkUrl(EditableItem);

            if (string.IsNullOrWhiteSpace(artworkUrl))
            {
                Alert.Error("No artwork found");
                return;
            }

            ArtworkUrl = artworkUrl;

            await OnUpdateUrl();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error loading artwork: " + ex.Message);
            Alert.Error("Error loading artwork: " + ex.StackTrace);
        }
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

        try
        {
            var image = await GetArtwork(ArtworkUrl);
            EditableItem.ArtworkUrl = ImageConverter.GetBase64UrlFromImage(image);
        }
        catch (Exception ex)
        {
            Alert.Error(ex.Message);
            return;
        }    
    }

    private async Task<ArtworkImage> GetArtwork(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            throw new Exception("URL is invalid");
        }

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
