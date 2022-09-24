using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models;

public class Link
{
    public Link(LinkType type, string name)
    {
        Type = type;
        Name = name;
    }

    public LinkType Type { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (obj is not Link link)
            return false;

        return link.Type == Type;
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode();
    }
}
