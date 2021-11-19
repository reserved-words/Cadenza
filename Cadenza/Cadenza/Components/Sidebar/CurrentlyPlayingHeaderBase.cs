namespace Cadenza;

public class CurrentlyPlayingHeaderBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    public string PlaylistName { get; set; }

    protected override void OnInitialized()
    {
        App.PlaylistUpdated += OnPlaylistUpdated;
    }

    private async Task OnPlaylistUpdated(object sender, PlaylistEventArgs e)
    {
        PlaylistName = e.PlaylistName;
        StateHasChanged();
    }
}