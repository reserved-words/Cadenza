using Cadenza.Common;

namespace Cadenza.Features.Tabs.Library.Components;

public class ArtistTabBase : FluxorComponent
{
    [Inject] public IState<ViewArtistState> ViewArtistState { get; set; }

    public bool Loading => ViewArtistState.Value.IsLoading;
    public ArtistDetailsVM Artist => ViewArtistState.Value.Artist;
    public IReadOnlyCollection<ArtistReleaseGroupVM> Releases => ViewArtistState.Value.Releases;

    protected List<LibraryBreadcrumbItem> Breadcrumbs => new List<LibraryBreadcrumbItem>
    {
        new LibraryBreadcrumbItem(PlayerItemType.Grouping, Artist.Grouping, Artist.Grouping),
        new LibraryBreadcrumbItem(PlayerItemType.Genre, Artist.Genre, Artist.Genre.GetGenreName()),
        new LibraryBreadcrumbItem(PlayerItemType.Artist, Artist.Id, Artist.Name)
    };
}
