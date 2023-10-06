using Cadenza.Common.Utilities.Interfaces;

namespace Cadenza.Common.Utilities.Services;

internal class NameComparer : INameComparer
{
    public string GetCompareName(string name)
    {
        name ??= "";
        name = name.ToLower();
        name = name.Replace(" & ", " and ");
        return name.StartsWith("the ")
            ? name[4..]
            : name;
    }
}