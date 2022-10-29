using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Tabs.Library;

public class TagTabBase : ComponentBase
{
    [Inject]
    public ITagRepository Repository { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    [Parameter]
    public string Id { get; set; }

    public List<PlayerItem> Items { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateTag();
    }

    private async Task UpdateTag()
    {
        Items = (await Repository.GetTag(Id))
            .OrderBy(i => i.Type)
            .ThenBy(i => i.Name)
            .ToList();

        StateHasChanged();
    }

    protected async Task OnViewItem(PlayerItem item)
    {
        await Viewer.ViewSearchResult(item);
    }
}
