﻿using System.Web;

namespace Cadenza.Features.Tabs.Edit.Components;

public class EditTrackLyricsBase : ComponentBase
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public EditableTrack Model { get; set; }

    private const string SearchUrl = "https://www.google.com/search?q=%22{0}%22+%22{1}%22+lyrics";

    private static bool AreEqual(string originalValue, string updatedValue)
    {
        if (originalValue == null && updatedValue == null)
            return true;

        if (originalValue == null || updatedValue == null)
            return false;

        return originalValue == updatedValue;
    }

    private string GetSearchUrl()
    {
        var artist = HttpUtility.UrlEncode(Model.ArtistName);
        var title = HttpUtility.UrlEncode(Model.Title);
        return string.Format(SearchUrl, artist, title);
    }

    protected Task OnLoad()
    {
        return Task.CompletedTask;
    }

    protected void OnSearch()
    {
        var searchUrl = GetSearchUrl();
        Dispatcher.Dispatch(new NavigationRequest(searchUrl, true));
    }
}
