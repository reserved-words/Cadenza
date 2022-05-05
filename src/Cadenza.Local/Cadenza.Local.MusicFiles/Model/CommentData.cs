namespace Cadenza.Local.MusicFiles.Model;

internal class CommentData
{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Website { get; set; }
    public string Twitter { get; set; }
    public string TrackYear { get; set; }
    public bool Instrumental { get; set; }
    public List<string> Tags { get; set; }
}