namespace Cadenza.Common;

public struct SourceId
{
    public SourceId(LibrarySource source, string id)
    {
        Source = source;
        Id = id;
    }

    public LibrarySource Source { get; set; }
    public string Id { get; set; }
}
