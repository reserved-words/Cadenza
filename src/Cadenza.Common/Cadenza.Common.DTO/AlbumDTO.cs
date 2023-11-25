namespace Cadenza.Common.DTO;

public class AlbumDTO
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public string Title { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
}
