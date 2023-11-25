namespace Cadenza.Common.DTO;

public class TrackDetailsDTO : TrackDTO
{
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public TagsDTO Tags { get; set; }
}