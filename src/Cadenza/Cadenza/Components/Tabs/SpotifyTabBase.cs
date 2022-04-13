using Cadenza.Components.Shared.Dialogs;
using Cadenza.Components.Shared;
using Cadenza.Components.Tabs.Spotify;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Components.Tabs;

public class SpotifyTabBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    public bool Loading { get; set; } = true;

    public List<AlbumInfo> Albums { get; set; } = new();

    public List<AlbumInfo> Playlists { get; set; } = new();

    public FullLibrary FullLibrary { get; set; }

    public List<DynamicTabsItem> FixedItems = new();

    public List<DynamicTabsItem> DynamicItems = new();

    public string SelectedItem { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Populate();

        FixedItems = new List<DynamicTabsItem>
        {
            new DynamicTabsItem("Playlists", "Playlists", Icon.Spotify, typeof(SpotifyPlaylists), new Dictionary<string, object>{ { "Items", Playlists } }),
            new DynamicTabsItem("Albums", "Albums", Icon.Spotify, typeof(SpotifyAlbums), new Dictionary<string, object>{ { "Items", Albums } })
        };
    }

    private async Task Populate()
    {
        Loading = true;
        FullLibrary = await Library.Get();
        Albums = FullLibrary.Albums.Where(a => a.ReleaseType != ReleaseType.Playlist).ToList();
        Playlists = FullLibrary.Albums.Where(a => a.ReleaseType == ReleaseType.Playlist).ToList();
        Loading = false;
    }

    protected async Task ShowArtist(SpotifyArtistProfile result)
    {
        await DialogService.Display<SpotifyArtistView, SpotifyArtistProfile>(result, $"Search Result - {result.Artist.Name}");
    }
}