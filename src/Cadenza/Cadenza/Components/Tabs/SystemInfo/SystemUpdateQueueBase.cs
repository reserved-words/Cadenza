namespace Cadenza;

public class SystemUpdateQueueBase : ComponentBase
{
    [Inject]
    public IFileUpdateQueue UpdateQueue { get; set; }

    public List<FileUpdateViewModel> Items { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var queue = await UpdateQueue.GetQueuedUpdates();
        if (queue.Updates == null)
            return;

        foreach (var update in queue.Updates)
        {
            Items.Add(new FileUpdateViewModel(update));
        }
    }

    protected async Task Remove(FileUpdateViewModel update)
    {
        var success = await UpdateQueue.RemoveQueuedUpdate(update.Update);
        if (success)
        {
            Items.Remove(update);
        }
    }

    protected bool FilterFunc(FileUpdateViewModel item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;

        if (item.Update.Item.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
