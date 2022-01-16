using System.Xml.Linq;

namespace Cadenza.Local;

public static class XElementExtensionMethods
{
    public static string GetValue(this XElement xml, string key)
    {
        return xml.Element(key)?.Value.Trim() ?? string.Empty;
    }
}