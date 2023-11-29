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