using Cadenza.Common;

namespace Cadenza.Features.Tabs.Library.Components;

public class GenreTabBase : FluxorComponent
{
    [Inject] public IState<ViewGenreState> ViewGenreState { get; set; }

    public bool Loading => ViewGenreState.Value.IsLoading;
    public GenreFullVM Genre => ViewGenreState.Value.Genre;
    public IReadOnlyCollection<ArtistVM> Artists => ViewGenreState.Value.Genre.Artists;

    protected string Id => Genre.Genre;
    protected string DisplayName => Genre.Genre.GetGenreName();

    protected List<LibraryBreadcrumbItem> Breadcrumbs => new List<LibraryBreadcrumbItem>
    {
        new LibraryBreadcrumbItem(PlayerItemType.Grouping, Genre.Grouping, Genre.Grouping),
        new LibraryBreadcrumbItem(PlayerItemType.Genre, Id, DisplayName)
    };
}
