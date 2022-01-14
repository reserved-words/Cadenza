namespace Cadenza.Domain;

public class Link
{
    public Link(LinkType type, string name)
    {
        Type = type;
        Name = name;
    }

    public LinkType Type { get; set; }
    public string Name { get; set; }
}
