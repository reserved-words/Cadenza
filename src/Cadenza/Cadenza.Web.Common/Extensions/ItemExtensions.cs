using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Extensions;

public static class ItemExtensions
{
    public static List<Disc> GroupByDisc(this List<AlbumTrack> tracks)
    {
        return tracks
            .GroupBy(t => t.DiscNo)
            .OrderBy(g => g.Key)
            .Select(r => new Disc
            {
                DiscNo = r.Key,
                Tracks = r.OrderBy(a => a.TrackNo)
                    .ToList()
            })
            .ToList();
    }

    public static List<ArtistReleaseGroup> GroupByReleaseType(this List<Album> albums)
    {
        return albums
            .GroupBy(a => a.ReleaseType.GetGroup())
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

    public static List<ArtistReleaseGroup> AddAlbumsFeaturingArtist(this List<ArtistReleaseGroup> groupedAlbums, List<Album> byOtherArtists)
    {
        if (byOtherArtists.Any())
        {
            groupedAlbums.Add(new ArtistReleaseGroup
            {
                Group = ReleaseTypeGroup.ByOtherArtists,
                Albums = byOtherArtists
            });
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

    public static string Location(this ArtistDetails artist)
    {
        if (artist == null)
            return "";

        return AsList(artist.City, artist.State, artist.Country);
    }

    public static string DiscPosition(this AlbumDetails album, AlbumTrackLink albumTrack)
    {
        var discCount = album.DiscCount == 0 ? 1 : album.DiscCount;
        return $"Disc {albumTrack.DiscNo} of {discCount}";
    }

    public static string TrackPosition(this AlbumDetails album, AlbumTrackLink albumTrack)
    {
        var trackCountIndex = albumTrack.DiscNo <= 0 ? 0 : albumTrack.DiscNo - 1;

        return album.TrackCounts.Count > trackCountIndex
                ? $"Track {albumTrack.TrackNo} of {album.TrackCounts[trackCountIndex]}"
                : $"Track {albumTrack.TrackNo} of ?";
    }

    private static string AsList(params string[] elements)
    {
        return string.Join(", ", elements.Where(s => !string.IsNullOrWhiteSpace(s)));
    }


}