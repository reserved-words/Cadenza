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
    public IItemViewer Viewer { get; set; }

    protected bool IsLoading { get; set; }

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

            _result = null;
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<SearchUpdateStartedEventArgs>(OnCacheUpdateStarted);
        Messenger.Subscribe<SearchUpdateCompletedEventArgs>(OnCacheUpdateCompleted);
    }

    private Task OnCacheUpdateCompleted(object sender, SearchUpdateCompletedEventArgs e)
    {
        IsLoading = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnCacheUpdateStarted(object sender, SearchUpdateStartedEventArgs e)
    {
        IsLoading = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected Task<IEnumerable<PlayerItem>> Search(string value)
    {
        if (value.IsCommon())
            return null;

        var results = Cache.Items
            .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .AsEnumerable();

        return Task.FromResult(results);
    }

    protected Task OnClear()
    {
        Result = null;
        return Task.CompletedTask;
    }
}

