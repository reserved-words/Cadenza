using Cadenza.Core.Model;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public SearchRepositoryCache Cache { get; set; }

    public bool IsLoading { get; set; }

    protected SourcePlayerItem Result { get; set; }

    protected override void OnInitialized()
    {
        Cache.UpdateStarted += Cache_UpdateStarted;
        Cache.UpdateCompleted += Cache_UpdateCompleted;
    }

    private void Cache_UpdateCompleted(object sender, EventArgs e)
    {
        IsLoading = false;
        StateHasChanged();
    }

    private void Cache_UpdateStarted(object sender, EventArgs e)
    {
        IsLoading = true;
        StateHasChanged();
    }

    protected Task<IEnumerable<SourcePlayerItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        var results = Cache.Items
            .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
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

    protected async Task OnClear()
    {
        Result = null;
    }
}

