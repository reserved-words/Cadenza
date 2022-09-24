using Cadenza.Web.Common.Interfaces;

namespace Cadenza.UI.Components.SystemInfo;

public class SystemUpdateQueueBase : ComponentBase
{
    [Inject]
    public IFileUpdateQueue UpdateQueue { get; set; }

    public List<ItemPropertyUpdate> Items { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var queue = await UpdateQueue.GetQueuedUpdates();
        if (queue == null)
            return;

        foreach (var update in queue)
        {
            Items.Add(update);
        }
    }

    protected async Task Remove(ItemPropertyUpdate update)
    {
        var success = await UpdateQueue.RemoveQueuedUpdate(update);
        if (success)
        {
            Items.Remove(update);
        }
    }

    protected bool FilterFunc(ItemPropertyUpdate update, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;

        if (update.Item.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
