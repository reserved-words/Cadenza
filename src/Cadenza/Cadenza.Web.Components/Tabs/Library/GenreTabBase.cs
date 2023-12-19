using Cadenza.Common;

namespace Cadenza.Web.Components.Tabs.Library;

public class GenreTabBase : FluxorComponent
{
    [Inject] public IState<ViewGenreState> ViewGenreState { get; set; }

    public bool Loading => ViewGenreState.Value.IsLoading;
    public GenreFullVM Genre => ViewGenreState.Value.Genre;
    public IReadOnlyCollection<ArtistVM> Artists => ViewGenreState.Value.Genre.Artists;

    protected string Id => Genre.Genre;
    protected string DisplayName => Genre.Genre.GetGenreName();

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Grouping, Genre.Grouping, Genre.Grouping),
        new LibraryBreadcrumb(PlayerItemType.Genre, Id, DisplayName)
    };
}
