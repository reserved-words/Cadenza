using Cadenza.Core.App;
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

    [Parameter]
    public Func<Task> OnClear { get; set; }

    protected async Task OnPlay()
    {
        switch (Result.Type)
        {
            case PlayerItemType.Grouping:
                await Player.PlayGrouping(Result.Id.Parse<Grouping>());
                break;
            case PlayerItemType.Genre:
                await Player.PlayGenre(Result.Id);
                break;
            case PlayerItemType.Artist:
                await Player.PlayArtist(Result.Id);
                break;
            case PlayerItemType.Album:
                await Player.PlayAlbum(Result.Source.Value, Result.Id);
                break;
            case PlayerItemType.Track:
                await Player.PlayTrack(Result.Source.Value, Result.Id);
                break;
            case PlayerItemType.Playlist:
                break;
        }
    }

    protected async Task OnViewItem()
    {
        await Viewer.ViewSearchResult(Result);
    }

    protected async Task Clear()
    {
        await OnClear();
    }
}