namespace Cadenza.API.SqlLibrary.Model;

internal class GetAlbumData : AlbumData
{
    public int Id { get; set; }
    public string ArtistNameId { get; set; }
    public string ArtistName { get; set; }
}
