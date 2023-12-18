namespace Cadenza.Web.Components.Tabs.Library;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistDetailsVM Artist => ViewArtistState.Value.Artist;
    public IReadOnlyCollection<ArtistReleaseGroupVM> Releases => ViewArtistState.Value.Releases;

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Grouping, Artist.Grouping.Id, Artist.Grouping.Name),
        new LibraryBreadcrumb(PlayerItemType.Genre, Artist.Grouping.Id, Artist.Genre),
        new LibraryBreadcrumb(PlayerItemType.Artist, Artist.Id, Artist.Name)
    };
}
