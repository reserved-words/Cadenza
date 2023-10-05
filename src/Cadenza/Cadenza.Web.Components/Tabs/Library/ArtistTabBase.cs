using Fluxor;

namespace Cadenza.Web.Components.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistDetails Artist => ViewArtistState.Value.Artist;
    public List<ArtistReleaseGroup> Releases => ViewArtistState.Value.Releases;
}
