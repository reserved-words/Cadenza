namespace Cadenza.Common.DTO;

public class UpdatedTrackPropertiesDTO
{
    public int TrackId { get; set; }

    public string Title { get; set; }

    public string Year { get; set; }

    public string Lyrics { get; set; }

    public TagsDTO Tags { get; set; }
}