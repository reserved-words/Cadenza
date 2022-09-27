namespace Cadenza.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ReleaseTypeGroupAttribute : Attribute
{
    public ReleaseTypeGroupAttribute(ReleaseTypeGroup group)
    {
        Group = group;
    }

    public ReleaseTypeGroup Group { get; private set; }
}
