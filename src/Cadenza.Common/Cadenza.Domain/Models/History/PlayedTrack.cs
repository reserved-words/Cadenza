namespace Cadenza.Domain;

public class PlayedTrack
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string ImageUrl { get; set; }
    public int Plays { get; set; }
    public int Rank { get; set; }
}
