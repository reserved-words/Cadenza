namespace Cadenza.Common;

public class Link : IMergeable
{
    public Link(LinkType type, string name)
    {
        Type = type;
        Name = name;
    }

    public LinkType Type { get; set; }
    public string Name { get; set; }

    public bool IsPopulated => Name != null;
    public string Id => Type.ToString();
}
