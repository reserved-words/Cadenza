using Cadenza.Domain.Model.Track;

namespace Cadenza.Web.Core.Model;

public class Disc
{
    public int DiscNo { get; set; }
    public List<AlbumTrack> Tracks { get; set; }
}