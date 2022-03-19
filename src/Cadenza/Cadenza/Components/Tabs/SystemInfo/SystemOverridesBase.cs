using Cadenza.Core.Interfaces;

namespace Cadenza;

public class SystemOverridesBase : ComponentBase
{
    [Inject]
    public IOverridesService Service { get; set; }

    public List<ItemPropertyUpdate> Items { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Items = await Service.GetOverrides();
    }

    protected async Task Remove(ItemPropertyUpdate dataOverride)
    {
        var success = await Service.RemoveOverride(dataOverride.Id, dataOverride.Property);
        if (success)
        {
            Items.Remove(dataOverride);
        }
    }

    protected bool FilterFunc(ItemPropertyUpdate item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (item.Property.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.OriginalValue.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.UpdatedValue.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}