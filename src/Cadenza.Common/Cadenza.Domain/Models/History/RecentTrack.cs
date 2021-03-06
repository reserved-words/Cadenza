namespace Cadenza.Domain;

public class RecentTrack
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public bool IsLoved { get; set; }
    public string ImageUrl { get; set; }
    public DateTime Played { get; set; }
    public bool NowPlaying { get; set; }
}
