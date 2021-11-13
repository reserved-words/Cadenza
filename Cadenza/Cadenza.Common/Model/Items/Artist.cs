namespace Cadenza.Common;

public class Artist : INamed
{
    public string Id { get; set; }
    public List<SourceId> SourceIds { get; set; } = new();

    [ItemProperty(ItemProperty.Artist)]
    public string Name { get; set; }

    [ItemProperty(ItemProperty.Grouping)]
    public Grouping Grouping { get; set; }

    [ItemProperty(ItemProperty.Genre)]
    public string Genre { get; set; }

    public override string ToString() => Name;

    public void AddSourceId(Source source, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"Cannot add an empty source ID");

        if (SourceIds.Any(s => s.Source == source
            && !string.IsNullOrWhiteSpace(id)))
            return;

        SourceIds.Add(new SourceId(source, id));
    }

    public void AddSourceId(SourceId sourceId)
    {
        AddSourceId(sourceId.Source, sourceId.Id);
    }
}
