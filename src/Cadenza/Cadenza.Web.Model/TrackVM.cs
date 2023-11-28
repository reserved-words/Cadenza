namespace Cadenza.Web.Model;

public record TrackVM
{
    public LibrarySource Source { get; init; }
    public int Id { get; init; }
    public string IdFromSource { get; init; }
    public int ArtistId { get; init; }
    public string Title { get; init; }
    public string ArtistName { get; init; }
    public int DurationSeconds { get; init; }
    public int AlbumId { get; init; }
    public bool IsLoved { get; set; }
}