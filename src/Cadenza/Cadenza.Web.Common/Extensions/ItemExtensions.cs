namespace Cadenza.Web.Common.Extensions;

public static class SearchExtensions
{
    public static bool IsCommon(this string searchTerm)
    {
        return searchTerm.Equals("the", StringComparison.InvariantCultureIgnoreCase)
            || searchTerm.Equals("the ", StringComparison.InvariantCultureIgnoreCase);
    }
}

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

    public static PlayerItemType? GetItemType(this PlaylistType playlistType)
    {
        return playlistType switch
        {
            PlaylistType.Album => PlayerItemType.Album,
            PlaylistType.Artist => PlayerItemType.Artist,
            PlaylistType.Genre => PlayerItemType.Genre,
            PlaylistType.Grouping => PlayerItemType.Grouping,
            PlaylistType.Tag => PlayerItemType.Tag,
            PlaylistType.Track => PlayerItemType.Track,
            _ => null
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