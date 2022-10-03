using Cadenza.Web.Common.Interfaces.Searchbar;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public ISearchCache Cache { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    public bool IsLoading { get; set; }

    protected PlayerItem Result { get; set; }

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
}

