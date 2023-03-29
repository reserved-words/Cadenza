namespace Cadenza.Common.Domain.Model.Updates;

public class PropertyUpdate
{
    public int Id { get; set; }
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}