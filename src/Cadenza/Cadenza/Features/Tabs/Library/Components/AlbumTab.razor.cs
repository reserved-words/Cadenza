using Cadenza.Common;

namespace Cadenza.Features.Tabs.Library.Components;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumFullVM Album => ViewAlbumState.Value.Album;

    protected Dictionary<int, int> DiscDurations => Album.Discs.ToDictionary(d => d.DiscNo, d => d.Tracks.Sum(t => t.DurationSeconds));

    protected List<LibraryBreadcrumbItem> Breadcrumbs => new List<LibraryBreadcrumbItem>
    {
        new LibraryBreadcrumbItem(PlayerItemType.Grouping, Album.Artist.Grouping, Album.Artist.Grouping),
        new LibraryBreadcrumbItem(PlayerItemType.Genre, Album.Artist.Genre, Album.Artist.Genre.GetGenreName()),
        new LibraryBreadcrumbItem(PlayerItemType.Artist, Album.Artist.Id, Album.Artist.Name),
        new LibraryBreadcrumbItem(PlayerItemType.Album, Album.Album.Id, Album.Album.Title)
    };
}
