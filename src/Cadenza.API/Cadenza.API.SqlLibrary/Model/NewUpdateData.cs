namespace Cadenza.API.SqlLibrary.Model;

internal class NewUpdateData
{
    public string Name { get; set; }
    public int SourceId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
