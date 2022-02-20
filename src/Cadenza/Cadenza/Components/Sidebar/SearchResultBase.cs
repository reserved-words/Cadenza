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

    protected async Task OnViewItem()
    {
        await Viewer.ViewSearchResult(Result);
    }

    protected async Task Clear()
    {
        await OnClear();
    }
}