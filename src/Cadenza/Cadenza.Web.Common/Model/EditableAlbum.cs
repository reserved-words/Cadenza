namespace Cadenza.Web.Common.Model;

public class EditableAlbum
{
    public int Id { get; init; }
    public int ArtistId { get; init; }
    public string ArtistName { get; init; }

    public string Title { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
    public string ArtworkBase64 { get; set; }
    public ICollection<string> Tags { get; set; }

    public string ImageUrl { get; set; }
}
