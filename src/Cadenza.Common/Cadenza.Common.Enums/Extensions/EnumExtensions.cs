﻿using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common.Enums.Extensions;

public static class EnumExtensions
{
    public static T GetAttribute<T>(this Enum en) where T : Attribute
    {
        var memInfo = en.GetType().GetMember(en.ToString());

        if (memInfo == null || memInfo.Length == 0)
            return null;

        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false).OfType<T>();

        return attributes.FirstOrDefault();
    }

    public static string GetDisplayName(this Enum en)
    {
        return en.GetAttribute<DisplayAttribute>()?.Name ?? en.ToString();
    }

    public static ReleaseTypeGroup GetGroup(this ReleaseType releaseType)
    {
        return releaseType.GetAttribute<ReleaseTypeGroupAttribute>().Group;
    }
}