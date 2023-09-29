using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Model;

public class Disc
{
    public int DiscNo { get; set; }
    public List<AlbumTrack> Tracks { get; set; }
}