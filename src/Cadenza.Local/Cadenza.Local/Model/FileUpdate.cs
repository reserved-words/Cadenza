namespace Cadenza.Local;

public class FileUpdate
{
    public ItemPropertyUpdate Update { get; set; }
    public List<FileUpdateFailedAttempt> FailedAttempts { get; set; } = new();
}
