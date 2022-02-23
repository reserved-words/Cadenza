using Cadenza.Domain;
using Cadenza.Utilities;

namespace Cadenza.Core.Extensions;

public static class ReleaseTypeExtensionMethods
{
    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>()?.Group ?? ReleaseTypeGroup.Albums;
    }
}
