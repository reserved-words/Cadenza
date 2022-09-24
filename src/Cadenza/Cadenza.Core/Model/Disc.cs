using Cadenza.Domain.Models.Track;

namespace Cadenza.Core.Model;

public class Disc
{
	public int DiscNo { get; set; }
	public List<AlbumTrack> Tracks { get; set; }
}