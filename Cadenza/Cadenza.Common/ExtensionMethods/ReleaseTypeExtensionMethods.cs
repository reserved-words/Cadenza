namespace Cadenza.Common;

public static class ReleaseTypeExtensionMethods
{
    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>()?.Group ?? Default.For<ReleaseTypeGroup>();
    }

    public static string GetGroupName(this ReleaseType releaseType)
    {
        return releaseType.GetGroup().GetDisplayName();
    }
}
