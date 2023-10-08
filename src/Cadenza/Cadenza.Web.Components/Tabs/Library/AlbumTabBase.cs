using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => ViewAlbumState.Value.Album;
    public IReadOnlyCollection<DiscVM> Discs => ViewAlbumState.Value.Discs;
}
