namespace Cadenza.Database.SqlLibrary.Model;

internal class AlbumData : AlbumDataBase
{
    public int Id { get; set; }
    public string ArtworkMimeType { get; set; }
    public byte[] ArtworkContent { get; set; }
}
