namespace Cadenza.Web.Components.Components.Sidebar;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IItemPlayer PlaylistPlayer { get; set; }

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }
}
