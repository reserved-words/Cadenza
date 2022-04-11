namespace Cadenza;

public class SpotifyTabBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    public bool Loading { get; set; } = true;

    public List<AlbumInfo> Albums { get; set; }

    public List<AlbumInfo> Playlists { get; set; }

    public FullLibrary FullLibrary {get; set;}

    protected override async Task OnInitializedAsync()
    {
        await Populate();
    }

    private async Task Populate()
    {
        try
        {
            Loading = true;
            FullLibrary = await Library.Get();
            Albums = FullLibrary.Albums.Where(a => a.ReleaseType != ReleaseType.Playlist).ToList();
            Playlists = FullLibrary.Albums.Where(a => a.ReleaseType == ReleaseType.Playlist).ToList();
            Loading = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}