using Cadenza.Common.Enums;

namespace Cadenza.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class ReleaseTypeGroupAttribute : Attribute
{
    public ReleaseTypeGroupAttribute(ReleaseTypeGroup group)
    {
        Group = group;
    }

    public ReleaseTypeGroup Group { get; private set; }
}
