﻿using Cadenza.Core.App;
using Cadenza.Core.Interop;
using Cadenza.Library;

namespace Cadenza;

public class EditLyricsBase : FormBase<TrackInfo>
{
    private const string SearchUrl = "https://www.google.com/search?q=%22{0}%22+%22{1}%22+lyrics";

    [Inject]
    public IMergedTrackRepository Repository { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IUpdatesController UpdatesService { get; set; }

    [Inject]
    public INavigation Navigation { get; set; }

    public TrackUpdate Update { get; set; }

    public TrackInfo EditableItem => Update.UpdatedItem;

    protected override void OnParametersSet()
    {
        Update = new TrackUpdate(Model);
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
            await UpdatesService.UpdateLyrics(Update);
            Submit();
        }
        catch (Exception ex)
        {
            // Log error
            Alert.Error("Error updating lyrics");
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
        return string.Format(SearchUrl, artist, title);
    }
}
