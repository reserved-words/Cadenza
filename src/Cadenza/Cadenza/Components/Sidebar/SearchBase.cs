using Cadenza.Core.Model;
using Cadenza.Database;

namespace Cadenza.Components.Sidebar;

public class SearchBase : ComponentBase
{
    [Inject]
    public SearchRepositoryCache Cache { get; set; }

    public bool IsLoading { get; set; }

    protected SourceSearchableItem Result { get; set; }

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

    protected Task<IEnumerable<SourceSearchableItem>> Search(string value)
    {
        if (IsCommon(value))
            return null;

        var results = Cache.Items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(results);
    }

    private static bool IsCommon(string value)
    {
        return value.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || value.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }
}

