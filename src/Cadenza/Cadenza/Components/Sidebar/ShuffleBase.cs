using Cadenza.Core.App;

namespace Cadenza;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IItemPlayer PlaylistPlayer { get; set; }

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }
}
