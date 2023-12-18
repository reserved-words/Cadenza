namespace Cadenza.Common.DTO;

public class ArtistDetailsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GroupingDTO Grouping { get; set; }
    public string Genre { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public TagsDTO Tags { get; set; }
}
