using Cadenza.Web.Core.Interfaces;

namespace Cadenza.UI.Components.Sidebar;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IItemPlayer PlaylistPlayer { get; set; }

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }
}
