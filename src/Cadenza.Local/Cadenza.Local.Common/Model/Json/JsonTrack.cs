using Cadenza.Domain;

namespace Cadenza.Local.Common.Model.Json;

public class JsonTrack
{
    public string Id { get; set; }
    public string Path { get; set; }
    public LibrarySource Source { get; set; }
    public string AlbumId { get; set; }
    public string ArtistId { get; set; }
    public string Title { get; set; }
    public int DurationSeconds { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public string Tags { get; set; }
}