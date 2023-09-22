using Fluxor;

namespace Cadenza.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public ArtistInfo Artist => ViewArtistState.Value.Artist;

    public List<ArtistReleaseGroup> Releases => ViewArtistState.Value.Releases;
}
