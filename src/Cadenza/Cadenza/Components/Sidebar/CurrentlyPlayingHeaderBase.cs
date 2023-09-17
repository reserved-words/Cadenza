using Cadenza.State.Store;
using Cadenza.Web.Common.Interfaces.View;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.Sidebar;

public class CurrentlyPlayingHeaderBase : FluxorComponent
{
    [Inject]
    public IState<PlaylistState> PlaylistState { get; set; }

    [Inject]
    public IItemViewer Viewer { get; set; }

    public string PlaylistName => PlaylistState.Value.Name;

    public bool ShowViewLink => PlaylistState.Value.Type != PlaylistType.All;

    public string Icon => GetIcon();

    protected async Task OnView()
    {
        if (PlaylistState.Value.Id == null)
            return;

        await Viewer.ViewPlaying(new PlaylistId(PlaylistState.Value.Id, PlaylistState.Value.Type, PlaylistState.Value.Name));
    }

    private string GetIcon()
    {
        if (PlaylistState.Value.Id == null)
            return null;

        var itemType = PlaylistState.Value.Type.GetItemType();

        if (!itemType.HasValue)
            return Icons.Material.Filled.Shuffle;

        return itemType.Value.GetIcon();
    }
}