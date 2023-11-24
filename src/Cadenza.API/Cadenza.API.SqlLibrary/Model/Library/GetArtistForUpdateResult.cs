namespace Cadenza.Database.SqlLibrary.Model.Library;

internal class GetArtistForUpdateResult
{
    public int Id { get; set; }
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
    public string Name { get; set; }
    public int GroupingId { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string TagList { get; set; }
}
