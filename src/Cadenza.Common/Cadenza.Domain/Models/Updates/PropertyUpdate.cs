using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models.Updates;

public class PropertyUpdate
{
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}