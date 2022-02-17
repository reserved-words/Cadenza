using Cadenza.Common;

namespace Cadenza;

public class SystemOverridesBase : ComponentBase
{
    [Inject]
    public Dictionary<LibrarySource, IOverridesService> Services { get; set; }

    public List<OverrideViewModel> Items { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        foreach (var source in Services.Keys)
        {
            var items = await Services[source].GetOverrides();
            if (items == null || !items.Any())
                continue;

            Items.AddRange(items.Select(i => new OverrideViewModel(source, i)));
        }
    }

    protected async Task Remove(OverrideViewModel dataOverride)
    {
        var service = Services[dataOverride.Source];
        var success = await service.RemoveOverride(dataOverride.Id, dataOverride.PropertyName);
        if (success)
        {
            Items.Remove(dataOverride);
        }
    }

    protected bool FilterFunc(OverrideViewModel item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (item.Source.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.PropertyName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.OriginalValue.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (item.OverrideValue.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}