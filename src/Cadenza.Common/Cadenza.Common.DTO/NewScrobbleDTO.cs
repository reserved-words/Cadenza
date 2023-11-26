namespace Cadenza.Common.DTO;

// TODO: DTOs like this are only used by the service, others only by the API - need to sort better
public class NewScrobbleDTO
{
    public int Id { get; set; }
    public string SessionKey { get; set; }
    public DateTime ScrobbledAt { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumArtist { get; set; }
}
