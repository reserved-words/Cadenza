namespace Cadenza.Web.Components.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistDetailsVM Artist => ViewArtistState.Value.Artist;
    public List<ArtistReleaseGroupVM> Releases => ViewArtistState.Value.Releases;
}
