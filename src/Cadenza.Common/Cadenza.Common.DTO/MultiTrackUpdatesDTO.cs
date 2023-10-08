namespace Cadenza.Common.DTO;

public class MultiTrackUpdatesDTO
{
    public List<string> TrackIds { get; set; }
    public List<PropertyUpdateDTO> Updates { get; set; } = new();
}
