namespace Cadenza.Local;

public class MetaDataUpdateResult
{
    public MetaDataUpdate Update { get; set; }
    public bool Completed { get; set; }
    public Exception Error { get; set; }
}