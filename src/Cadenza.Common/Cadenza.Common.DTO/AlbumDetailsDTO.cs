namespace Cadenza.Common.DTO;

public class AlbumDetailsDTO : AlbumDTO
{
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; }

    [ItemProperty(ItemProperty.AlbumTags)]
    public TagsDTO Tags { get; set; }
}
