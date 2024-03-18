namespace Cadenza.Features.Tabs.Edit.Components;

public class EditAlbumTracksBase : ComponentBase
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public EditableAlbumDiscs Model { get; set; }
    [Parameter] public List<int> RemovedTracks { get; set; }

    protected IReadOnlyCollection<ReleaseType> ReleaseTypes => Enum.GetValues<ReleaseType>();

    public void OnTrackNoChanged(EditableAlbumTrack track)
    {
        Model.SortAll();
    }

    public void OnDiscNoChanged(EditableAlbumTrack track)
    {
        var currentDisc = Model.GetDisc(track);
        if (currentDisc.DiscNo == track.DiscNo)
            return;

        var newDisc = Model.GetDisc(track.DiscNo);
        RemoveTrackFromDisc(currentDisc, track);
        UpdateDiscCount();
        AddTrackToDisc(newDisc, track);
        Model.SortAll();
    }

    public void OnRemoveTrack(EditableAlbumTrack track)
    {
        var currentDisc = Model.GetDisc(track);
        RemoveTrackFromDisc(currentDisc, track);
        UpdateDiscCount();
        RemovedTracks.Add(track.TrackId);
    }

    private void AddTrackToDisc(EditableAlbumDisc disc, EditableAlbumTrack track)
    {
        disc.AddTrack(track);
        disc.UpdateTrackCount();
    }

    private void RemoveTrackFromDisc(EditableAlbumDisc disc, EditableAlbumTrack track)
    {
        disc.Tracks.Remove(track);
        disc.UpdateTrackCount();
        Model.RemoveIfEmpty(disc);
    }

    private void UpdateDiscCount()
    {
        Dispatcher.Dispatch(new AlbumUpdateDiscCountRequest(Model.GetDiscCount()));
    }
}
