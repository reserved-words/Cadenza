namespace Cadenza.API.SqlLibrary.Model;

internal class NewArtistData : ArtistDataBase
{
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
}
