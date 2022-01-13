using System.Web;
using System.Xml.Linq;

namespace Cadenza.LastFM;

internal static class ExtensionMethods
{
    public static string SetMethod(this string url, string name)
    {
        var encodedValue = HttpUtility.UrlEncode(name);
        return $"{url}?method={encodedValue}";
    }

    public static string Add(this string url, string key, string value)
    {
        var encodedValue = HttpUtility.UrlEncode(value);
        return $"{url}&{key}={encodedValue}";
    }

    public static string Add(this string baseUrl, Dictionary<string, string> parameters)
    {
        var pairs = parameters.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}");
        var queryString = string.Join("&", pairs);
        return $"{baseUrl}?{queryString}";
    }

    public static async Task<XElement> ToXml(this HttpResponseMessage response)
    {
        var contentAsString = await response.Content.ReadAsStringAsync();

        var xml = XDocument.Parse(contentAsString);

        var root = xml.Element("lfm");
        if (root.Attribute("status").Value == "failed")
        {
            var error = root.Element("error");
            throw new Exception(error.Value);
        }

        return root;
    }
}