using Cadenza.Utilities;
using Cadenza.Database;
using Cadenza.Core.Model;

namespace Cadenza.Components.Sidebar;

public class SearchResultBase : ComponentBase
{

    [Inject]
    public IItemPlayer Player { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    [Parameter]
    public SourcePlayerItem Result { get; set; }

    protected async Task OnPlay()
    {
        if (Result.Type == PlayerItemType.Artist)
        {
            await Player.PlayArtist(Result.Id);
        }
        else if (Result.Type == PlayerItemType.Album)
        {
            await Player.PlayAlbum(Result.Source.Value, Result.Id);
        }
        else if (Result.Type == PlayerItemType.Track)
        {
            await Player.PlayTrack(Result.Source.Value, Result.Id);
        }
    }

    protected async Task OnView()
    {
        await Viewer.ViewSearchResult(Result);
    }
}