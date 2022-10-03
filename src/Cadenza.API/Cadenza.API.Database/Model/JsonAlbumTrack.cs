namespace Cadenza.API.Database.Model;

internal class JsonAlbumTrack
{
    public string TrackId { get; set; }
    public string AlbumId { get; set; }
    public int TrackNo { get; set; }
    public int? DiscNo { get; set; }
}
