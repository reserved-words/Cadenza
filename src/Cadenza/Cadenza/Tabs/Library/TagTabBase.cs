namespace Cadenza.Tabs.Library;

public class TagTabBase : ComponentBase
{
    [Inject]
    public ITagRepository Repository { get; set; }

    [Parameter]
    public string Id { get; set; }

    public List<PlayerItem> Albums { get; set; } = new();
    public List<PlayerItem> Artists { get; set; } = new();
    public List<PlayerItem> Tracks { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateTag();
    }

    private async Task UpdateTag()
    {
        var items = await Repository.GetTag(Id);

        Albums = items.Where(i => i.Type == PlayerItemType.Album).ToList();
        Artists = items.Where(i => i.Type == PlayerItemType.Artist).ToList();
        Tracks = items.Where(i => i.Type == PlayerItemType.Track).ToList();

        StateHasChanged();
    }
}
