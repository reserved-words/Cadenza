using Cadenza.Core;

namespace Cadenza;

public class CurrentlyPlayingHeaderBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    public string PlaylistName { get; set; } = "Nothing";

    protected override void OnInitialized()
    {
        App.PlaylistStarted += OnPlaylistUpdated;
    }

    private Task OnPlaylistUpdated(object sender, PlaylistEventArgs e)
    {
        PlaylistName = e.PlaylistName;
        StateHasChanged();
        return Task.CompletedTask;
    }
}