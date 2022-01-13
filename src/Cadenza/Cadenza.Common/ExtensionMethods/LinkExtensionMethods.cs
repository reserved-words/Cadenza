using System.Web;

namespace Cadenza.Common;

public static class LinkExtensionMethods
{
    public static string GetDefault(this LinkType type, string artistName)
    {
        if (artistName == null)
            return null;

        return type switch
        {
            LinkType.BandsInTown => artistName.Replace(" ", ""),
            LinkType.Search => artistName.UrlEncode(),
            LinkType.LastFm => artistName.UrlEncode(),
            LinkType.Wikipedia => artistName.Replace(" ", "_").UrlEncode(),
            _ => artistName.Replace(" ", "")
        };
    }

    public static string GetUrl(this LinkType type, string artistName)
    {
        return type switch
        {
            LinkType.LastFm => $"https://www.last.fm/music/{artistName}",
            LinkType.BandsInTown => $"https://www.bandsintown.com/{artistName}",
            LinkType.BandCamp => $"https://{artistName}.bandcamp.com/",
            LinkType.YouTube => $"https://www.youtube.com/user/{artistName}",
            LinkType.Twitter => $"https://twitter.com/{artistName}",
            LinkType.Facebook => $"https://facebook.com/{artistName}",
            LinkType.Search => $"https://www.google.com/search?q=%22{artistName}%22",
            LinkType.Wikipedia => $"https://en.wikipedia.org/wiki/{artistName}",
            _ => throw new NotImplementedException($"No URL format found for link type {type}")
        };
    }

    private static string UrlEncode(this string name)
    {
        return HttpUtility.UrlEncode(name);
    }

}