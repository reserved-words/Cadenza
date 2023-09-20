using Cadenza.State.Actions;
using Cadenza.Web.Common.Interfaces.Searchbar;
using Fluxor;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public ISearchCache Cache { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

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
                Dispatcher.Dispatch(new ViewItemRequest(Result.Type, Result.Id, Result.Name));
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
}

