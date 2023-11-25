namespace Cadenza.Database.SqlLibrary.Model.Queue;

internal class GetAlbumUpdatesResult
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
