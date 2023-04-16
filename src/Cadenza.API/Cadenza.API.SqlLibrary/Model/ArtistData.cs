namespace Cadenza.API.SqlLibrary.Model;

internal class ArtistData : ArtistDataBase
{
    public int Id { get; set; }
    public string NameId { get; set; }
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
}
