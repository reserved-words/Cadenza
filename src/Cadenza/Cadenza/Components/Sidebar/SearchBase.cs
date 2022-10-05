using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public ISearchCache Cache { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    public bool IsLoading { get; set; }

    private PlayerItem _result;

    protected PlayerItem Result 
    {
        get { return _result; }
        set
        {
            _result = value;

            if (_result != null)
            {
                Viewer.ViewSearchResult(Result);
            }
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<SearchUpdateStartedEventArgs>(Cache_UpdateStarted);
        Messenger.Subscribe<SearchUpdateCompletedEventArgs>(Cache_UpdateCompleted);
    }

    private Task Cache_UpdateCompleted(object sender, SearchUpdateCompletedEventArgs e)
    {
        IsLoading = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task Cache_UpdateStarted(object sender, SearchUpdateStartedEventArgs e)
    {
        IsLoading = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected Task<IEnumerable<PlayerItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        var results = Cache.Items
            .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .AsEnumerable();

        return Task.FromResult(results);
    }

    private static bool IsCommon(string value)
    {
        return value.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || value.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }

    protected Task OnClear()
    {
        Result = null;
        return Task.CompletedTask;
    }

    protected async Task OnPlay()
    {
        if (Result == null)
            return;

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
                await Player.PlayAlbum(Result.Id);
                break;
            case PlayerItemType.Track:
                await Player.PlayTrack(Result.Id);
                break;
            case PlayerItemType.Playlist:
                break;
        }
    }

    //protected async Task OnViewItem()
    //{
    //    await Viewer.ViewSearchResult(Result);
    //}
}

