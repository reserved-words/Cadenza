using Cadenza.Web.Model;
using Cadenza.Web.State.Actions;
using System.Collections.ObjectModel;

namespace Cadenza.Web.Components.Forms.Track;

public class EditTrackBase : FormBase<TrackDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    public EditableTrack EditableItem { get; set; }

    protected override void OnParametersSet()
    {
        EditableItem = new EditableTrack
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
        var updatedTrack = Model with
        {
            Title = EditableItem.Title,
            Year = EditableItem.Year,
            Lyrics = EditableItem.Lyrics,
            Tags = new ReadOnlyCollection<string>(EditableItem.Tags.ToList())
        };

        Dispatcher.Dispatch(new TrackUpdateRequest(Model, updatedTrack));
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
