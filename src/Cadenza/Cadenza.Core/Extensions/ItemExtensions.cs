using Cadenza.Core;
using Cadenza.Core.Model;

namespace Cadenza.Common;

public static class ItemExtensions
{
    public static List<ArtistReleaseGroup> GroupByReleaseType(this List<Album> albums)
    {
        return albums
            .GroupBy(a => a.ReleaseType.GetAttribute<ReleaseTypeGroupAttribute>().Group)
            .Select(r => new ArtistReleaseGroup
            {
                Group = r.Key,
                Albums = r.OrderBy(a => a.ReleaseType)
                    .ThenBy(a => a.Year)
                    .ToList()
            })
            .ToList();
    }

    public static List<LinkViewModel> Links(this ArtistInfo artist)
    {
        if (artist == null)
            return new List<LinkViewModel>();

        return Enum.GetValues<LinkType>()
            .Select(lt => artist.GetLinkViewModel(lt))
            .ToList();
    }

    private static LinkViewModel GetLinkViewModel(this ArtistInfo artist, LinkType linkType)
    {
        var link = artist?.Links?.FirstOrDefault(l => l.Type == linkType);
        var name = link?.Name ?? linkType.GetDefault(artist.Name);
        var url = linkType.GetUrl(name);

        return new LinkViewModel
        {
            Type = linkType,
            Url = url,
            Disabled = link != null && link.Name == null
        };
    }

    public static string Location(this ArtistInfo artist)
    {
        if (artist == null)
            return "";

        return AsList(artist.City, artist.State, artist.Country);
    }

    private static string AsList(params string[] elements)
    {
        return string.Join(", ", elements.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}