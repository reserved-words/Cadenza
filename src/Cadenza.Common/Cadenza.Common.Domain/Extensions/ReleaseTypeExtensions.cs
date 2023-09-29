namespace Cadenza.Common.Domain.Extensions;
public static class ReleaseTypeExtensions
{
    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>().Group;
    }
}
