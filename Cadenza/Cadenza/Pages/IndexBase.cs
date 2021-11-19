using Cadenza.Player;

namespace Cadenza;

public class IndexBase : ComponentBase
{
    [Inject]
    public IAppController App { get; set; }

    public bool Loading { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        Loading = true;
        await App.Initialise();
        Loading = false;
    }

    public async Task OnPlay(PlaylistDefinition playlistDefinition)
    {
        await App.Play(playlistDefinition);
    }

    public async Task OnPause()
    {
        await App.Pause();
    }

    public async Task OnResume()
    {
        await App.Resume();
    }

    public async Task OnSkipNext()
    {
        await App.SkipNext();
    }

    public async Task OnSkipPrevious()
    {
        await App.SkipPrevious();
    }

    public async Task OnSourcesUpdated(List<LibrarySource> enabledSources)
    {
        await App.UpdateSources(enabledSources);
    }
}