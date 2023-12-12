using System.Collections.ObjectModel;

namespace Cadenza.Web.Common.Extensions;

public static class ItemExtensions
{
    private static IReadOnlyCollection<T> ToReadOnlyList<T>(this IEnumerable<T> items)
    {
        return new ReadOnlyCollection<T>(items.ToList());
    }

    public static List<AlbumDiscVM> GroupByDisc(this List<AlbumTrackVM> tracks)
    {
        return tracks.GroupBy(t => t.DiscNo)
            .Select(d => new AlbumDiscVM
            {
                DiscNo = d.Key,
                TrackCount = d.First().DiscTrackCount,
                Tracks = d.ToList()
            })
            .OrderBy(d => d.DiscNo)
            .ToList();
    }

    public static List<ArtistReleaseGroupVM> GroupByReleaseType(this List<AlbumVM> albums)
    {
        return albums
            .GroupBy(a => a.ReleaseType.GetGroup())
            .OrderBy(g => g.Key)
            .Select(r => new ArtistReleaseGroupVM(r.Key, r.OrderBy(a => a.ReleaseType)
                .ThenBy(a => a.Year)
                .ToReadOnlyList()))
            .ToList();
    }

    public static List<ArtistReleaseGroupVM> AddAlbumsFeaturingArtist(this List<ArtistReleaseGroupVM> groupedAlbums, List<AlbumVM> byOtherArtists)
    {
        if (byOtherArtists.Any())
        {
            groupedAlbums.Add(new ArtistReleaseGroupVM(ReleaseTypeGroup.ByOtherArtists, byOtherArtists));
        }

        return groupedAlbums;
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

    public static string Location(this ArtistDetailsVM artist)
    {
        if (artist == null)
            return "";

        return AsList(artist.City, artist.State, artist.Country);
    }

    public static string DiscPosition(this AlbumTrackLinkVM albumTrack)
    {
        var discCount = albumTrack.DiscCount == 0 ? 1 : albumTrack.DiscCount;
        return $"Disc {albumTrack.DiscNo} of {discCount}";
    }

    public static string TrackPosition(this AlbumTrackLinkVM albumTrack)
    {
        return $"Track {albumTrack.TrackNo} of {albumTrack.TrackCount}";
    }

    private static string AsList(params string[] elements)
    {
        return string.Join(", ", elements.Where(s => !string.IsNullOrWhiteSpace(s)));
    }


}