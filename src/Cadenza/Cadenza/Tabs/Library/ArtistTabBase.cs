using Cadenza.Common.Domain.Model.Library;
using Fluxor;

namespace Cadenza.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistInfo Artist => ViewArtistState.Value.Artist;
    public List<ArtistReleaseGroup> Releases => ViewArtistState.Value.Releases;
}
