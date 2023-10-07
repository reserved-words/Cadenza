namespace Cadenza.Common.DTO;

public class TrackUpdateDTO : ItemUpdateDTO<TrackDetailsDTO>
{
    public TrackUpdateDTO()
        : base() { }

    public TrackUpdateDTO(TrackDetailsDTO track)
        : base(LibraryItemType.Track, track.Id, track) { }
}
