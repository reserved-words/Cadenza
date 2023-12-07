namespace Cadenza.Web.Components.Tabs.Edit;

public class EditAlbumTabBase : FluxorComponent
{
    [Inject] public IState<EditAlbumState> EditAlbumState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public bool Loading => EditAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => EditAlbumState.Value.Album;
    public IReadOnlyCollection<AlbumDiscVM> Tracks => EditAlbumState.Value.Tracks;

    protected EditableAlbum EditableAlbum { get; set; }
    protected List<EditableAlbumDisc> EditableTracks { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditAlbumResult>(OnEditAlbumFetched);
        base.OnInitialized();
    }

    private void OnEditAlbumFetched(FetchEditAlbumResult result)
    {
        if (Album == null)
            return;

        EditableAlbum = new EditableAlbum
        {
            Id = Album.Id,
            ArtistId = Album.ArtistId,
            ArtistName = Album.ArtistName,
            Title = Album.Title,
            ReleaseType = Album.ReleaseType,
            Year = Album.Year,
            ArtworkBase64 = Album.ArtworkBase64,
            Tags = Album.Tags.ToList()
        };

        EditableTracks = Tracks
            .Select(d => new EditableAlbumDisc
            {
                DiscNo = d.DiscNo,
                TrackCount = d.TrackCount,
                Tracks = d.Tracks.Select(t => new EditableAlbumTrack
                {
                    TrackId = t.TrackId,
                    TrackNo = t.TrackNo,
                    Title = t.Title,
                    DurationSeconds = t.DurationSeconds,
                    ArtistId = t.ArtistId,
                    ArtistName = t.ArtistName,
                    IdFromSource = t.IdFromSource
                }).ToList()
            })
            .ToList();
    }
}
