namespace Cadenza.Web.Common.ViewModels;

public class TrackUpdateVM : ItemUpdateVM<TrackDetailsVM>
{
    public TrackUpdateVM()
        : base() { }

    public TrackUpdateVM(TrackDetailsVM track)
        : base(LibraryItemType.Track, track.Id, track) { }
}
