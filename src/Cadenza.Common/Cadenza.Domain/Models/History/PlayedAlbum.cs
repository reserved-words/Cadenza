namespace Cadenza.Domain;

public class PlayedAlbum
{
    public string Artist { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public int Plays { get; set; }
    public int Rank { get; set; }
}
