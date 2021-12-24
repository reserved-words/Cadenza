using System.ComponentModel.DataAnnotations;

namespace Cadenza.Database;

public class DbPlayTracks
{
    [Key]
    public string Id { get; set; }

    public string Tracks { get; set; }
}
