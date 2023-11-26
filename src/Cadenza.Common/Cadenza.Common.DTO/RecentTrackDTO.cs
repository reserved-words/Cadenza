namespace Cadenza.Common.DTO;

public class RecentTrackDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public int AlbumId { get; set; }
    public string Album { get; set; }
    public bool IsLoved { get; set; }
    public DateTime Played { get; set; }
    public bool NowPlaying { get; set; }
}
