using Cadenza.Utilities;
using Cadenza.Database;

namespace Cadenza.Components.Sidebar;

public class SearchResultBase : ComponentBase
{
    [Inject]
    public INotificationService Notifications { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Inject]
    public IPlaylistPlayer Player { get; set; }

    [Inject]
    public IIdGenerator IdGenerator { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public SourceSearchableItem Result { get; set; }

    [Parameter]
    public LibrarySource? ResultSource { get; set; }

    [Parameter]
    public Func<SearchableItemType, string, string, Task> OnViewItem { get; set; }

    public bool DisplayArtistLink => Result != null && Result.Type != SearchableItemType.Playlist;
    public bool DisplayAlbumLink => Result != null && Result.Type != SearchableItemType.Artist;
    public bool DisplayTrackLink => Result != null && Result.Type == SearchableItemType.Track;

    protected async Task OnPlay()
    {
        try
        {
            await Play();
        }
        catch (ConnectorException ex)
        {
            // await App.DisableConnector(ex.Connector, ex.Error, ex.Message);
        }
    }

    private async Task Play()
    {
        if (Result.Type == SearchableItemType.Artist)
        {
            await Player.PlayArtist(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Album)
        {
            await Player.PlayAlbum(ResultSource.Value, Result.Id);
        }
        else if (Result.Type == SearchableItemType.Playlist)
        {
            await Player.PlayPlaylist(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Track)
        {
            await Player.PlayTrack(ResultSource.Value, Result.Id);
        }
    }

    protected async Task OnViewTrack()
    {
        await OnViewItem(SearchableItemType.Track, Result.Id, Result.Name);
    }

    protected async Task OnViewAlbum()
    {
        var albumId = Result.Type == SearchableItemType.Album
            ? Result.Id
            : IdGenerator.GenerateId(Result.Artist, Result.Album);


        var albumTitle = Result.Type == SearchableItemType.Album
            ? Result.Name
            : Result.Album;

        await OnViewItem(SearchableItemType.Album, albumId, albumTitle);
    }

    protected async Task OnViewArtist()
    {
        var artistId = Result.Type == SearchableItemType.Artist
            ? Result.Id
            : IdGenerator.GenerateId(Result.Artist);

        var artistName = Result.Type == SearchableItemType.Artist
            ? Result.Name
            : Result.Artist;

        await OnViewItem(SearchableItemType.Artist, artistId, artistName);
    }
}

