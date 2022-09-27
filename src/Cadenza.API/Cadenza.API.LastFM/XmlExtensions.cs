using System.Globalization;
using System.Xml.Linq;

namespace Cadenza.API.LastFM;

public static class XmlExtensions
{
    private static readonly string[] ImageSizes = new string[] { "extralarge", "large", "medium", "small" };
    private static readonly IFormatProvider Format = CultureInfo.InvariantCulture.DateTimeFormat;

    public static string Get(this XElement xml, string parent, string name, bool isAttribute = false)
    {
        return xml.Element(parent).Get(name, isAttribute);
    }

    public static string Get(this XElement xml, string name, bool isAttribute = false)
    {
        return isAttribute
            ? xml.Attribute(name)?.Value
            : xml.Element(name)?.Value;
    }

    public static int GetInt(this XElement xml, string name, bool isAttribute = false)
    {
        return int.TryParse(xml.Get(name, isAttribute), out int result)
            ? result
            : 0;
    }

    public static bool GetBool(this XElement xml, string name, bool isAttribute = false)
    {
        var value = xml.Get(name, isAttribute);
        return value == "1" || value == "true";
    }

    public static DateTime GetDateTime(this XElement xml, string name, bool isAttribute = false)
    {
        var value = xml.Get(name);

        return DateTime.TryParseExact(value, "dd MMM yyyy, HH:mm", Format, DateTimeStyles.AssumeUniversal, out DateTime result)
            ? result
            : DateTime.MinValue;
    }

    public static string GetImage(this XElement xml)
    {
        var images = xml.Elements("image");

        foreach (var size in ImageSizes)
        {
            var image = images.FirstOrDefault(im => im.Attribute("size").Value == size);
            if (image != null)
                return image.Value;
        }

        return null;
    }
}