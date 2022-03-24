namespace Cadenza.Domain;

public class TrackUpdate : ItemUpdate<TrackInfo>
{
    public TrackUpdate()
        : base() { }

    public TrackUpdate(TrackInfo track)
        : base(LibraryItemType.Track, track.Id, track.Title, track) { }
}
