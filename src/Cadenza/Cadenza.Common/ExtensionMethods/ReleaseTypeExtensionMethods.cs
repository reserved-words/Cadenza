using Cadenza.Domain;
using Cadenza.Utilities;

namespace Cadenza.Common;

public static class ReleaseTypeExtensionMethods
{
    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>()?.Group ?? ReleaseTypeGroup.Albums;
    }
}
