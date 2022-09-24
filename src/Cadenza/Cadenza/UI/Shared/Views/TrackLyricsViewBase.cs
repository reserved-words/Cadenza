using Cadenza.Core.Updates;
using Cadenza.Domain.Models.Track;

namespace Cadenza.UI.Shared.Views;

public class TrackLyricsViewBase : ComponentBase
{
    [Inject]
    public IUpdatesConsumer Updates { get; set; }

    [Parameter]
    public TrackInfo Model { get; set; }

    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();

    protected override void OnInitialized()
    {
        Updates.LyricsUpdated += OnLyricsUpdated;
    }

    private Task OnLyricsUpdated(object sender, LyricsUpdatedEventArgs e)
    {
        if (Model != null && e.Update.Id == Model.Id)
        {
            e.Update.ApplyUpdates(Model);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
