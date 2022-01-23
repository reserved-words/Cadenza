using Cadenza.Common;
using Cadenza.Core;
using Cadenza.Utilities;

namespace Cadenza.Components.Sidebar;

public class SearchResultBase : ComponentBase
{
    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Inject]
    public IPlaylistPlayer Player { get; set; }

    [Inject]
    public IIdGenerator IdGenerator { get; set; }

    [Parameter]
    public SearchableItem Result { get; set; }

    protected async Task OnPlay()
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

