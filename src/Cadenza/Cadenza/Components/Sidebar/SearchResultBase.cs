using Cadenza.Common;
using Cadenza.Utilities;

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

    [Parameter]
    public SearchableItem Result { get; set; }

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
            await Player.PlayAlbum(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Playlist)
        {
            await Player.PlayPlaylist(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Track)
        {
            var albumId = IdGenerator.GenerateId(Result.Artist, Result.Album);
            await Player.PlayTrack(Result.Id, albumId);
        }
    }
}

