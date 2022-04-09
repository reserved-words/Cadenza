using Cadenza.Core.Common;

namespace Cadenza;

public static class Extensions
{
    public static string GetIcon(this Connector connector)
    {
        return connector switch
        {
            Connector.API => Icons.Material.Filled.Api,
            Connector.Local => LibrarySource.Local.GetIcon(),
            Connector.Spotify => LibrarySource.Spotify.GetIcon(),
            Connector.LastFm => "fab fa-lastfm-square"
        };
    }
    
    public static string GetIcon(this LibrarySource source)
    {
        return source switch
        {
            LibrarySource.Local => Icons.Material.Filled.Home,
            LibrarySource.Spotify => "fab fa-spotify"
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

    public static string GetIcon(this PlayerItemType type)
	{
        return type switch
        {
            PlayerItemType.Artist => "fas fa-users", // Icons.Material.Filled.PeopleAlt,
            PlayerItemType.Album => "fas fa-compact-disc", // Icons.Material.Filled.Album,
            PlayerItemType.Track => "fas fa-music", // Icons.Material.Filled.MusicNote,
            PlayerItemType.Playlist => "fas fa-list-ol",
            PlayerItemType.Grouping => "fas fa-box",
            PlayerItemType.Genre => "fas fa-boxes",
        };
    }

    public static string WithLineBreaks(this string text)
    {
        return text?.Replace("\n", "<br />", StringComparison.InvariantCultureIgnoreCase);
    }

    public static string Duration(this AlbumTrack track)
    {
        var duration = TimeSpan.FromSeconds(track.DurationSeconds);

        return duration.TotalHours > 1
            ? duration.ToString(@"hh\:mm\:ss")
            : duration.ToString(@"mm\:ss");
    }
}