using Cadenza.Common.Enums;
using Cadenza.Web.Components;
using Cadenza.Web.State.Store;
using Fluxor;

namespace Cadenza.Web.Components.Main.Sidebar;

public class CurrentlyPlayingHeaderBase : FluxorComponent
{
    [Inject] public IState<PlaylistState> PlaylistState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public string PlaylistName => PlaylistState.Value.Name;

    public bool ShowViewLink => PlaylistState.Value.Type != PlaylistType.All;

    public string Icon => GetIcon();

    protected void OnView()
    {
        if (PlaylistState.Value.Id == null)
            return;

        var type = PlaylistState.Value.Type.GetItemType();

        if (!type.HasValue)
            return;

        Dispatcher.Dispatch(new ViewItemRequest(type.Value, PlaylistState.Value.Id, PlaylistState.Value.Name));
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