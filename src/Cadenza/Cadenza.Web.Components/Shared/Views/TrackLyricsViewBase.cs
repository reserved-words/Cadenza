using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Components.Shared.Views;

public class TrackLyricsViewBase : ComponentBase
{
    [Inject]
    public IMessenger Messenger { get; set; }

    [Parameter]
    public TrackInfo Model { get; set; }

    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();

    protected override void OnInitialized()
    {
        Messenger.Subscribe<LyricsUpdatedEventArgs>(OnLyricsUpdated);
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
