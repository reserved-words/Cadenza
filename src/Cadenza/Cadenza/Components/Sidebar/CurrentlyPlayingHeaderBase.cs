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
    public IMessenger Messenger { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    public string PlaylistName => _playlist?.Name ?? "Nothing";

    public bool ShowViewLink => _playlist.HasValue && _viewableTypes.Contains(_playlist.Value.Type);

    private PlaylistId? _playlist = null;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<PlaylistLoadingEventArgs>(OnPlaylistLoading);
        Messenger.Subscribe<PlaylistStartedEventArgs>(OnPlaylistStarted);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);
    }

    private Task OnPlaylistFinished(object sender, PlaylistFinishedEventArgs e)
    {
        _playlist = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnPlaylistLoading(object sender, PlaylistLoadingEventArgs e)
    {
        _playlist = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnPlaylistStarted(object sender, PlaylistStartedEventArgs e)
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