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
        catch (SourceException ex)
        {
            Notifications.Error(ex.Message);
            await App.StopPlaylist();
            // TODO:
            // Set the source to disabled
            // If the current album / playlist is purely for this source, stop playing
            // If try to play albums / playlists / artists from this source, display error again
            // In the UI disable all albums / playlists / artists that are only for a disabled source
            // Add a check when skip to a new track - if in a disabled source, skip again
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

