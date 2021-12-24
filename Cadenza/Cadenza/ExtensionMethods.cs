namespace Cadenza;

public static class ExtensionMethods
{
    public static string GetIcon(this LibrarySource source)
    {
        return source switch
        {
            LibrarySource.Local => Icons.Material.Filled.Home,
            LibrarySource.Spotify => Icons.Material.Filled.Wifi,
            LibrarySource.Cloud => Icons.Material.Filled.Cloud,
            _ => throw new NotImplementedException()
        };
    }

    public static string GetIcon(this LinkViewModel link)
    {
        return link.Type switch
        {
            LinkType.LastFm => "fab fa-lastfm-square",
            LinkType.BandsInTown => Icons.Filled.Event,
            LinkType.BandCamp => "fab fa-bandcamp",
            LinkType.YouTube => Icons.Custom.Brands.YouTube,
            LinkType.Twitter => Icons.Custom.Brands.Twitter,
            LinkType.Facebook => Icons.Custom.Brands.Facebook,
            LinkType.Wikipedia => "fab fa-wikipedia-w",
            LinkType.Search => Icons.Custom.Brands.Google
        };
    }

    public static string WithLineBreaks(this string text)
    {
        return text?.Replace("\n", "<br />", StringComparison.InvariantCultureIgnoreCase);
    }
}