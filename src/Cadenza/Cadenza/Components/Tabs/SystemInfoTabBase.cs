namespace Cadenza.Components.Tabs;

public class SystemInfoTabBase : ComponentBase
{
    public List<string> Items { get; set; }

    protected override void OnInitialized()
    {
        Items = Enumerable.Range(0, 500)
            .Select(i => $"Item {i}")
            .ToList();
    }
}