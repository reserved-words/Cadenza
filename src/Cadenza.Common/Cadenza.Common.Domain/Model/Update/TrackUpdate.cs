using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Common.Domain.Model.Update;

public class TrackUpdate : ItemUpdate<TrackInfo>
{
    public TrackUpdate()
        : base() { }

    public TrackUpdate(TrackInfo track)
        : base(LibraryItemType.Track, track.Id, track.Title, track) { }
}
