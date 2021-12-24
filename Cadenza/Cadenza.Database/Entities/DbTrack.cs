using Cadenza.Common;
using System.ComponentModel.DataAnnotations;

namespace Cadenza.Database;

public class DbTrack
{
    [Key]
    public string Id { get; set; }
    public string Summary { get; set; }
    public string Details { get; set; }
}

