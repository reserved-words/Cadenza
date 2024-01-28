namespace Cadenza.Common.DTO;

public class LovedTrackUpdateDTO
{
    public int TrackId { get; set; }
    public string SessionKey { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public bool IsLoved { get; set; }
}
