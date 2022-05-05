using Cadenza.Domain;

namespace Cadenza.Local.Common.Model;

public class ItemPropertyUpdateResult
{
    public ItemPropertyUpdate Update { get; set; }
    public bool Completed { get; set; }
    public Exception Error { get; set; }
}