namespace Cadenza.State.Model;

public record TrackUpdateVM : ItemUpdateVM<TrackDetailsVM>
{
    public TrackUpdateVM(TrackDetailsVM track)
        : base(LibraryItemType.Track, track.Id, track) { }
}
