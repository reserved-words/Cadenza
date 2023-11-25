namespace Cadenza.Common.DTO;

public class ArtistDetailsDTO : ArtistDTO
{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public TagsDTO Tags { get; set; }
}
