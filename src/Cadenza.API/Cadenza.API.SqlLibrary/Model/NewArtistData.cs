namespace Cadenza.API.SqlLibrary.Model;

internal class NewArtistData : ArtistDataBase
{
    public string CompareName { get; set; }
    public string GroupingName { get; set; }
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
}
