using Cadenza.Domain;

namespace Cadenza.Core.Model;

public class FileUpdate
{
    public ItemPropertyUpdate Update { get; set; }
    public List<FileUpdateFailedAttempt> FailedAttempts { get; set; } = new();
}
