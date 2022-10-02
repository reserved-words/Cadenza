using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Components.Sidebar;

public class CurrentlyPlayingHeaderBase : ComponentBase
{
    private readonly PlaylistType[] _viewableTypes = new PlaylistType[]
    {
        PlaylistType.Artist,
        PlaylistType.Album,
        PlaylistType.Track
    };

    [Inject]
    public IPlayMessenger App { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    public string PlaylistName => _playlist?.Name ?? "Nothing";

    public bool ShowViewLink => _playlist.HasValue && _viewableTypes.Contains(_playlist.Value.Type);

    private PlaylistId? _playlist = null;

    protected override void OnInitialized()
    {
        App.PlaylistStarted += OnPlaylistUpdated;
    }

    private Task OnPlaylistUpdated(object sender, PlaylistEventArgs e)
    {
        _playlist = e.Playlist;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task OnView()
    {
        if (!_playlist.HasValue)
            return;

        await Viewer.ViewPlaying(_playlist.Value);
    }
}