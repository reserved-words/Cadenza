using Cadenza.Source.Spotify;
using Cadenza.Source.Spotify.Model;
using Cadenza.UI.Shared;
using Cadenza.UI.Tabs.Spotify;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.UI.Tabs.Main;

public class SpotifyTabBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    [Inject]
    public ISpotifySearcher Searcher { get; set; }

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
            new DynamicTabsItem("Albums", "Albums", Icon.Spotify, typeof(SpotifyAlbumsTab), new Dictionary<string, object>{ { "Items", Albums } }),
            new DynamicTabsItem("Playlists", "Playlists", Icon.Spotify, typeof(SpotifyPlaylistsTab), new Dictionary<string, object>{ { "Items", Playlists } })
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

    protected async Task ShowAlbum(SpotifyAlbum album)
    {
        if (!DynamicItems.Any(t => t.Id == album.Id))
        {
            var tracks = await Searcher.GetAlbumTracks(album.Id);
            var result = new SpotifyAlbumSearchResult
            {
                Album = album,
                Tracks = tracks
            };
            DynamicItems.Add(GetAlbumTab(result));
        }

        SelectedItem = album.Id;
        StateHasChanged();
    }

    protected Task ShowArtist(SpotifyArtistSearchResult result)
    {
        if (!DynamicItems.Any(t => t.Id == result.Artist.Id))
        {
            DynamicItems.Add(GetArtistTab(result));
        }

        SelectedItem = result.Artist.Id;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected async Task ShowPlaylist(SpotifyPlaylist playlist)
    {
        if (!DynamicItems.Any(t => t.Id == playlist.Id))
        {
            var tracks = await Searcher.GetPlaylistTracks(playlist.Id);
            var result = new SpotifyPlaylistSearchResult
            {
                Playlist = playlist,
                Tracks = tracks
            };
            DynamicItems.Add(GetPlaylistTab(result));
        }

        SelectedItem = playlist.Id;
        StateHasChanged();
    }

    private DynamicTabsItem GetAlbumTab(SpotifyAlbumSearchResult result)
    {
        return new DynamicTabsItem(result.Album.Id, result.Album.Title, PlayerItemType.Album.GetIcon(), typeof(SpotifyAlbumTab), new Dictionary<string, object>
        {
            { "Model", result },
        });
    }

    private DynamicTabsItem GetArtistTab(SpotifyArtistSearchResult result)
    {
        foreach (var playlist in result.Playlists)
        {
            playlist.IsInLibrary = Playlists.Any(p => p.Id == playlist.Id);
        }

        foreach (var album in result.Albums)
        {
            album.IsInLibrary = Albums.Any(a => a.Id == album.Id);
        }

        return new DynamicTabsItem(result.Artist.Id, result.Artist.Name, PlayerItemType.Artist.GetIcon(), typeof(SpotifyArtistTab), new Dictionary<string, object>
        {
            { "Model", result },
            { "OnShowAlbum", new Func<SpotifyAlbum, Task>(ShowAlbum) }
        });
    }

    private DynamicTabsItem GetPlaylistTab(SpotifyPlaylistSearchResult result)
    {
        return new DynamicTabsItem(result.Playlist.Id, result.Playlist.Title, PlayerItemType.Playlist.GetIcon(), typeof(SpotifyPlaylistTab), new Dictionary<string, object>
        {
            { "Model", result },
        });
    }
}