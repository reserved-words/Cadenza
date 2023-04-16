namespace Cadenza.API.SqlLibrary.Model;

internal class NewArtistData : ArtistDataBase
{
    public string NameId { get; set; }
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
}
