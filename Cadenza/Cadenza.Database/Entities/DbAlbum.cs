using Cadenza.Common;
using System.ComponentModel.DataAnnotations;

namespace Cadenza.Database;

public class DbAlbum
{
    public string Id { get; set; }
    public string ArtistId { get; set; }
    public string Title { get; set; }
    public string Artwork { get; set; }
    public string Year { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public LibrarySource Source { get; set; }
    public ICollection<DbDisc> Discs { get; set; } = new List<DbDisc>();

    public virtual DbArtist Artist { get; set; }
    public virtual ICollection<DbTrack> Tracks { get; set; } = new List<DbTrack>();
}

public class DbDisc
{
    public int DiscNo { get; set; }
    public int TrackCount { get; set; }
}