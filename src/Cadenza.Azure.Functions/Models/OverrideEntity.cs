using Microsoft.Azure.Cosmos.Table;

namespace Cadenza.Azure.Functions.Models;

public class OverrideEntity : TableEntity
{
    public string ItemType { get; set; }
    public string Item { get; set; }
    public string OriginalValue { get; set; }
    public string OverrideValue { get; set; }
}