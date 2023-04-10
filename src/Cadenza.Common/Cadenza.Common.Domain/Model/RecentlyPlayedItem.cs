namespace Cadenza.Common.Domain.Model;

public class RecentlyPlayedItem
{
    public int TypeId { get; set; }
    public string ItemId { get; set; }
    public string PlaylistName { get; set; }
    public string ArtistName { get; set; }
    public string AlbumTitle { get; set; }
}
