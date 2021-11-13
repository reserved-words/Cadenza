namespace Cadenza.Common;

public class FileUpdate
{
    public MetaDataUpdate Update { get; set; }
    public List<FileUpdateFailedAttempt> FailedAttempts { get; set; } = new();
}
