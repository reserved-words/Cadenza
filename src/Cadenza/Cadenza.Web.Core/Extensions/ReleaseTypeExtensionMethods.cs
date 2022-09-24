using Cadenza.Domain.Attributes;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Extensions;

namespace Cadenza.Web.Core.Extensions;

public static class ReleaseTypeExtensionMethods
{
    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>()?.Group ?? ReleaseTypeGroup.Albums;
    }
}
