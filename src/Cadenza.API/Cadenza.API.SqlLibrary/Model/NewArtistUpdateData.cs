namespace Cadenza.Database.SqlLibrary.Model;

internal class NewArtistUpdateData
{
    public int ArtistId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
