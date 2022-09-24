using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;

namespace Cadenza.Core.Model;

public class ArtistReleaseGroup
{
	public ReleaseTypeGroup Group { get; set; }
	public List<Album> Albums { get; set; }
}
