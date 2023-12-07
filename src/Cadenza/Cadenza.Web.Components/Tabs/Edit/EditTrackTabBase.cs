namespace Cadenza.Web.Components.Tabs.Edit;

public class EditTrackTabBase : FluxorComponent
{
    [Inject] public IState<EditTrackState> EditTrackState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public bool Loading => EditTrackState.Value.IsLoading;
    public TrackDetailsVM Track => EditTrackState.Value.Track;

    protected EditableTrack EditableTrack { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<FetchEditTrackResult>(OnEditTrackFetched);
        base.OnInitialized();
    }

    private void OnEditTrackFetched(FetchEditTrackResult result)
    {
        if (Track == null)
            return;

        EditableTrack = new EditableTrack
        {
            Id = Track.Id,
            ArtistId = Track.ArtistId,
            ArtistName = Track.ArtistName,
            Title = Track.Title,
            Year = Track.Year,
            AlbumId = Track.AlbumId,
            DurationSeconds = Track.DurationSeconds,
            IdFromSource = Track.IdFromSource,
            Lyrics = Track.Lyrics,
            Source = Track.Source,
            Tags = Track.Tags.ToList()
        };
    }
}
