using Cadenza.Common.Domain.Enums;

namespace Cadenza.Common.Domain.Model.Updates;

public class PropertyUpdate
{
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}