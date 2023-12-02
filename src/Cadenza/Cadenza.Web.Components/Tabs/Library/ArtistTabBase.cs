using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistDetailsVM Artist => ViewArtistState.Value.Artist;
    public IReadOnlyCollection<ArtistReleaseGroupVM> Releases => ViewArtistState.Value.Releases;
}
