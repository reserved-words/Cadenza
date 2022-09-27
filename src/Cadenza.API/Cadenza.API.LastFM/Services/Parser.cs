using System.Globalization;
using System.Xml.Linq;

namespace Cadenza.API.LastFM.Services;

internal class Parser : IParser
{
    private static readonly string[] ImageSizes = new string[] { "extralarge", "large", "medium", "small" };
    private static readonly IFormatProvider Format = CultureInfo.InvariantCulture.DateTimeFormat;

    public string Get(XElement xml, string name, string propertyName, bool isAttribute = false)
    {
        var xmlParent = xml.Element(name);
        return Get(xmlParent, propertyName, isAttribute);
    }

    public string Get(XElement xml, string propertyName, bool isAttribute = false)
    {
        return isAttribute
            ? xml.Attribute(propertyName)?.Value
            : xml.Element(propertyName)?.Value;
    }

    public int GetInt(XElement xml, string propertyName, bool isAttribute = false)
    {
        return int.TryParse(Get(xml, propertyName, isAttribute), out int result)
            ? result
            : 0;
    }

    public bool GetBool(XElement xml, string propertyName, bool isAttribute = false)
    {
        var value = Get(xml, propertyName, isAttribute);
        return value == "1" || value == "true";
    }

    public DateTime GetDateTime(XElement xml, string propertyName)
    {
        var value = Get(xml, propertyName);

        return DateTime.TryParseExact(value, "dd MMM yyyy, HH:mm", Format, DateTimeStyles.AssumeUniversal, out DateTime result)
            ? result
            : DateTime.MinValue;
    }

    public string GetImage(XElement xml)
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