using Cadenza.Domain.Models.Update;
using Cadenza.Interfaces;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Interop;
using Cadenza.Web.Core.Interfaces;

namespace Cadenza.UI.Shared.Dialogs;

public class EditLyricsBase : FormBase<TrackInfo>
{
    private const string SearchUrl = "https://www.google.com/search?q=%22{0}%22+%22{1}%22+lyrics";

    [Inject]
    public IUpdateService Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesController UpdatesService { get; set; }

    [Inject]
    public INavigation Navigation { get; set; }

    public TrackUpdate Update { get; set; }

    public TrackInfo EditableItem => Update.UpdatedItem;

    public List<DialogAction> Actions { get; set; } = new();

    protected override void OnInitialized()
    {
        Actions = new List<DialogAction>
        {
            new DialogAction("Load", OnLoad),
            new DialogAction("Search", OnSearch)
        };
    }

    protected override void OnParametersSet()
    {
        Update = new TrackUpdate(Model);
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

            await Repository.UpdateTrack(Update);
            Alert.Success("Lyrics updated");
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating lyrics: " + ex.Message);
        }

        await UpdatesService.UpdateLyrics(Update);
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(SearchUrl, artist, title);
    }
}
