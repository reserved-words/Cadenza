namespace Cadenza.Web.Components.Forms.Track;

public class EditTrackBase : FormBase<TrackDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    public EditableTrack EditableItem => GetEditableItem();

    private EditableTrack GetEditableItem()
    {
        return new EditableTrack
        {
            Id = Model.Id,
            ArtistId = Model.ArtistId,
            ArtistName = Model.ArtistName,
            Title = Model.Title,
            Year = Model.Year,
            AlbumId = Model.AlbumId,
            DurationSeconds = Model.DurationSeconds,
            IdFromSource = Model.IdFromSource,
            Lyrics = Model.Lyrics,
            Source = Model.Source,
            Tags = Model.Tags.ToList()
        };
    }

    protected override void OnInitialized()
    {
        SubscribeToAction<TrackUpdatedAction>(OnTrackUpdated);
        base.OnInitialized();
    }

    protected void OnSubmit()
    {
        Dispatcher.Dispatch(new TrackUpdateRequest(Model, EditableItem));
    }

    private void OnTrackUpdated(TrackUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
