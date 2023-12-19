namespace Cadenza.Common.DTO;

public class PlaylistDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<int> TrackIds { get; set; }
}