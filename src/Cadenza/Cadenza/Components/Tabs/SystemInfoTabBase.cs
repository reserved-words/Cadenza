namespace Cadenza;

public class SystemInfoTabBase : ComponentBase
{
    public List<string> Items { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Items = Enumerable.Range(0, 500)
            .Select(i => $"Item {i}")
            .ToList();
    }
}