namespace Cadenza.Common.Domain.Model.Updates;

public class EditedProperty
{
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}