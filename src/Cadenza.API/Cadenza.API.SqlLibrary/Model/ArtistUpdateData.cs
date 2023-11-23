namespace Cadenza.Database.SqlLibrary.Model;

internal class ArtistUpdateData
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
