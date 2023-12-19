namespace Cadenza.Common.DTO;

public class GenreDTO
{
    public string Genre { get; set; }
    public string Grouping { get; set; }
    public List<ArtistDTO> Artists { get; set; }
}