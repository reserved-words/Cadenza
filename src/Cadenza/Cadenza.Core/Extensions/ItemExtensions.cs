using Cadenza.Core;
using Cadenza.Core.Model;

namespace Cadenza.Common;

public static class ItemExtensions
{
    public static List<Disc> GroupByDisc(this List<AlbumTrack> tracks)
    {
        return tracks
            .GroupBy(t => t.Position.DiscNo)
            .OrderBy(g => g.Key)
            .Select(r => new Disc
            {
                DiscNo = r.Key,
                Tracks = r.OrderBy(a => a.Position.TrackNo)
                    .ToList()
            })
            .ToList();
    }

    public static List<ArtistReleaseGroup> GroupByReleaseType(this List<Album> albums)
    {
        return albums
            .GroupBy(a => a.ReleaseType.GetAttribute<ReleaseTypeGroupAttribute>().Group)
            .OrderBy(g => g.Key)
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

    public static string DiscPosition(this AlbumInfo album, AlbumTrackPosition position)
    {
        return $"Disc {position.DiscNo} of {album.DiscCount}";
    }

    public static string TrackPosition(this AlbumInfo album, AlbumTrackPosition position)
    {
        var trackCountIndex = position.DiscNo - 1;
        return $"Track {position.TrackNo} of {album.TrackCounts[trackCountIndex]}";
    }

    private static string AsList(params string[] elements)
    {
        return string.Join(", ", elements.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}