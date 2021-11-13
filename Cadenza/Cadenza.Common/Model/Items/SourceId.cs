namespace Cadenza.Common;

public struct SourceId
{
    public SourceId(Source source, string id)
    {
        Source = source;
        Id = id;
    }

    public Source Source { get; set; }
    public string Id { get; set; }
}
