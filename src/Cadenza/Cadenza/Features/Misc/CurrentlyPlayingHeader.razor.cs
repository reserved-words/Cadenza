namespace Cadenza.Features.Misc;

public class CurrentlyPlayingHeaderBase : FluxorComponent
{
    [Inject] public IState<PlaylistState> PlaylistState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public string PlaylistName => PlaylistState.Value.Name;

    public bool ShowViewLink => PlaylistState.Value.Type != PlaylistType.All && ItemType != null;

    public string PlaylistTypeIcon => GetIcon();

    public object Id => PlaylistState.Value.Id;

    public PlayerItemType? ItemType => PlaylistState.Value.Type.GetItemType();

    private string GetIcon()
    {
        if (PlaylistState.Value.Id == null)
            return null;

        return Icons.Material.Filled.PlaylistPlay;

        // This stuff doesn't work for some reason - for now just return one specific icon

        var itemType = PlaylistState.Value.Type.GetItemType();

        if (!itemType.HasValue)
            return Icon.Shuffle;

        return itemType.Value.GetIcon();
    }
}