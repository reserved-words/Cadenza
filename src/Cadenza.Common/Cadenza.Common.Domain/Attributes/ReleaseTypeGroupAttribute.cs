namespace Cadenza.Common.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class ReleaseTypeGroupAttribute : Attribute
{
    public ReleaseTypeGroupAttribute(ReleaseTypeGroup group)
    {
        Group = group;
    }

    public ReleaseTypeGroup Group { get; private set; }
}
