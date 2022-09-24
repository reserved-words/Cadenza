using Cadenza.Interfaces;
using Cadenza.Web.Core.Interfaces;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.UI.Shared.Menus;

public class MenuGenreBase : ComponentBase
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

    public Task OnEdit()
    {
        return Task.CompletedTask;
    }

    public async Task OnPlay()
    {
        await Player.PlayGenre(Id);
    }
}
