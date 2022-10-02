using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Components.Sidebar;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IItemPlayer PlaylistPlayer { get; set; }

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }
}
