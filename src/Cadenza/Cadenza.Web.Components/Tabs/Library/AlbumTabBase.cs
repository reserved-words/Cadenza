namespace Cadenza.Web.Components.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => ViewAlbumState.Value.Album;
    public IReadOnlyCollection<AlbumDiscVM> Discs => ViewAlbumState.Value.Tracks;

    protected Dictionary<int, int> DiscDurations => Discs.ToDictionary(d => d.DiscNo, d => d.Tracks.Sum(t => t.DurationSeconds));

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Artist, Album.ArtistId, Album.ArtistName),
        new LibraryBreadcrumb(PlayerItemType.Album, Album.Id, Album.Title)
    };
}
