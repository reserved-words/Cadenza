using System.Xml.Linq;

namespace Cadenza.Local.MusicFiles.Extensions;

internal static class XElementExtensions
{
    public static string GetValue(this XElement xml, string key)
    {
        return xml.Element(key)?.Value.Trim() ?? string.Empty;
    }
}