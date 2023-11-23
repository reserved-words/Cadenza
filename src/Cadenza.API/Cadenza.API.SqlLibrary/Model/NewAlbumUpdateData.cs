namespace Cadenza.Database.SqlLibrary.Model;

internal class NewAlbumUpdateData
{
    public int AlbumId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
