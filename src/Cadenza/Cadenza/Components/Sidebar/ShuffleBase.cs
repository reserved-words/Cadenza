using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Components.Sidebar;

public class ShuffleBase : ComponentBase
{
    [Inject]
    public IItemPlayer PlaylistPlayer { get; set; }

    public IEnumerable<Grouping> Groupings => Enum.GetValues<Grouping>()
        .Where(g => g != Grouping.None)
        .OrderBy(g => g.ToString());

    public async Task OnShuffleAll()
    {
        await PlaylistPlayer.PlayAll();
    }

    public async Task OnShuffleGrouping(Grouping grouping)
    {
        await PlaylistPlayer.PlayGrouping(grouping);
    }
}
