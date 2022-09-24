using Cadenza.Web.Common.Enums;
using Cadenza.Web.Core.Common;

namespace Cadenza;

public static class Extensions
{
    public static string GetIcon(this Connector connector)
    {
        return connector switch
        {
            Connector.API => Icons.Material.Filled.Api,
            Connector.Local => LibrarySource.Local.GetIcon(),
            Connector.LastFm => Icon.LastFm,
            _ => throw new NotImplementedException()
        };
    }
    
    public static string GetIcon(this LibrarySource source)
    {
        return source switch
        {
            LibrarySource.Local => Icons.Material.Filled.Home,
            _ => throw new NotImplementedException()
        };
    }

    public static string GetIcon(this LinkViewModel link)
    {
        return link.Type switch
        {
            LinkType.LastFm => Icon.LastFm,
            LinkType.BandsInTown => Icons.Filled.Event,
            LinkType.BandCamp => Icon.BandCamp,
            LinkType.YouTube => Icons.Custom.Brands.YouTube,
            LinkType.Twitter => Icons.Custom.Brands.Twitter,
            LinkType.Facebook => Icons.Custom.Brands.Facebook,
            LinkType.Wikipedia => Icon.Wikipedia,
            LinkType.Search => Icons.Custom.Brands.Google,
            _ => throw new NotImplementedException()
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
            _ => throw new NotImplementedException(),
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

    public static RenderFragment RenderFragment(this Type type, Dictionary<string, object> parameters = null)
    {
        return (builder) => 
        {
            var sequence = 0;
            builder.OpenComponent(sequence++, type);

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    builder.AddAttribute(sequence++, p.Key, p.Value);
                }
            }

            builder.CloseComponent(); 
        };
    }
}