using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Domain.Model.Updates;

public class TrackUpdate : ItemUpdate<TrackDetails>
{
    public TrackUpdate()
        : base() { }

    public TrackUpdate(TrackDetails track)
        : base(LibraryItemType.Track, track.Id, track) { }
}
