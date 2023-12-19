using Cadenza.Common;

namespace Cadenza.Web.Components.Tabs.Library;

public class GenreTabBase : FluxorComponent
{
    [Inject] public IState<ViewGenreState> ViewGenreState { get; set; }

    public bool Loading => ViewGenreState.Value.IsLoading;
    public GenreFullVM Genre => ViewGenreState.Value.Genre;
    public IReadOnlyCollection<ArtistVM> Artists => ViewGenreState.Value.Genre.Artists;

    protected string GenreId => (Genre.Grouping, Genre.Genre).GenreId();
    protected string GenreName => Genre.Genre;

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Grouping, Genre.Grouping, Genre.Grouping),
        new LibraryBreadcrumb(PlayerItemType.Genre, GenreId, GenreName)
    };
}
