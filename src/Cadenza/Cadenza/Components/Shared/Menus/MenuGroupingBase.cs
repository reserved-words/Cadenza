using Cadenza.Core.App;

namespace Cadenza;

public class MenuGroupingBase : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public INotificationService Alert { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string Id { get; set; }

    public async Task OnEdit()
    {
    }

    public async Task OnPlay()
    {
        await Player.PlayGrouping(Id.Parse<Grouping>());
    }
}
