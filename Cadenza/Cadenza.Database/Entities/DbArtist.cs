using Cadenza.Common;
using System.ComponentModel.DataAnnotations;

namespace Cadenza.Database;

public class DbArtist
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public Grouping Grouping { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public ICollection<LibrarySource> Sources { get; set; }

    public virtual ICollection<DbTrack> Tracks { get; set; }
    public virtual ICollection<DbAlbum> Albums { get; set; }
}
