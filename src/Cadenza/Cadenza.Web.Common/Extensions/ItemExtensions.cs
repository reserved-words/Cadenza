using Cadenza.Common.Domain.Attributes;
using Cadenza.Common.Domain.Extensions;
using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Web.Common.Extensions;

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

    public static string Location(this ArtistInfo artist)
    {
        if (artist == null)
            return "";

        return AsList(artist.City, artist.State, artist.Country);
    }

    public static string DiscPosition(this AlbumInfo album, AlbumTrackPosition position)
    {
        var discNo = position.DiscNo == 0 ? 1 : position.DiscNo;
        var discCount = album.DiscCount == 0 ? 1 : album.DiscCount;
        return $"Disc {discNo} of {discCount}";
    }

    public static string TrackPosition(this AlbumInfo album, AlbumTrackPosition position)
    {
        var trackCountIndex = (position.DiscNo == 0 ? 1 : position.DiscNo) - 1;

        return album.TrackCounts.Count > trackCountIndex
                ? $"Track {position.TrackNo} of {album.TrackCounts[trackCountIndex]}"
                : $"Track {position.TrackNo} of ?";
    }

    private static string AsList(params string[] elements)
    {
        return string.Join(", ", elements.Where(s => !string.IsNullOrWhiteSpace(s)));
    }


}