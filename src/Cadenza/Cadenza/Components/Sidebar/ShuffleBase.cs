using Cadenza.Core;

namespace Cadenza;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IPlaylistPlayer PlaylistPlayer { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }
}
