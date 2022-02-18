namespace Cadenza.Core.Model;

public class ArtistReleaseGroup
{
	public ReleaseTypeGroup Group { get; set; }
	public List<AlbumInfo> Albums { get; set; }
}
