namespace Cadenza;

public class SpotifyTabBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    public bool Loading { get; set; } = true;

    public List<AlbumInfo> Albums { get; set; }

    public List<AlbumInfo> Playlists { get; set; }

    private FullLibrary _library;

    protected override async Task OnInitializedAsync()
    {
        await Populate();
    }

    private async Task Populate()
    {
        Loading = true;
        _library = await Library.Get();
        Albums = _library.Albums.Where(a => a.ReleaseType != ReleaseType.Playlist).ToList();
        Playlists = _library.Albums.Where(a => a.ReleaseType == ReleaseType.Playlist).ToList();
        Loading = false;
    }
}