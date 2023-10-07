namespace Cadenza.Web.Common.Model;

public class EditableTrack
{
    public LibrarySource Source { get; init; }
    public int Id { get; init; }
    public string IdFromSource { get; init; }
    public int ArtistId { get; init; }
    public string ArtistName { get; init; }
    public int DurationSeconds { get; init; }
    public int AlbumId { get; init; }

    public string Title { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public ICollection<string> Tags { get; set; }
}