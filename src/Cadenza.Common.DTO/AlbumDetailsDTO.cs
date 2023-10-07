namespace Cadenza.Common.DTO;

public class AlbumDetailsDTO : AlbumDTO
{
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();

    [ItemProperty(ItemProperty.AlbumTags)]
    public TagListDTO Tags { get; set; } = new TagListDTO();
}
