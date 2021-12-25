namespace Cadenza.Common;

public class ArtistLinks
{
    public string ArtistId { get; set; }
    public List<string> Albums { get; set; } = new List<string>();
    public List<string> Tracks { get; set; } = new List<string>();
}
