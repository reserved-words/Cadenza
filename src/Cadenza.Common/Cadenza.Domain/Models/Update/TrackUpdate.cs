using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Domain.Models.Update;

public class TrackUpdate : ItemUpdate<TrackInfo>
{
    public TrackUpdate()
        : base() { }

    public TrackUpdate(TrackInfo track)
        : base(LibraryItemType.Track, track.Id, track.Title, track) { }
}
