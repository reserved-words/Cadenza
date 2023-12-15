namespace Cadenza.Common.DTO;

public class UpdatedArtistPropertiesDTO
{
    public int ArtistId { get; set; }

    public string GroupingName { get; set; }

    public string Genre { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string ImageBase64 { get; set; }

    public TagsDTO Tags { get; set; }
}