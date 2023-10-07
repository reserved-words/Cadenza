namespace Cadenza.Web.Common.Extensions;

public static class ItemExtensions
{
    public static List<DiscVM> GroupByDisc(this List<AlbumTrackVM> tracks)
    {
        return tracks
            .GroupBy(t => t.DiscNo)
            .OrderBy(g => g.Key)
            .Select(r => new DiscVM
            {
                DiscNo = r.Key,
                Tracks = r.OrderBy(a => a.TrackNo)
                    .ToList()
            })
            .ToList();
    }

    public static List<ArtistReleaseGroupVM> GroupByReleaseType(this List<AlbumVM> albums)
    {
        return albums
            .GroupBy(a => a.ReleaseType.GetGroup())
            .OrderBy(g => g.Key)
            .Select(r => new ArtistReleaseGroupVM
            {
                Group = r.Key,
                Albums = r.OrderBy(a => a.ReleaseType)
                    .ThenBy(a => a.Year)
                    .ToList()
            })
            .ToList();
    }

    public static List<ArtistReleaseGroupVM> AddAlbumsFeaturingArtist(this List<ArtistReleaseGroupVM> groupedAlbums, List<AlbumVM> byOtherArtists)
    {
        if (byOtherArtists.Any())
        {
            groupedAlbums.Add(new ArtistReleaseGroupVM
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

    public static string Location(this ArtistDetailsVM artist)
    {
        if (artist == null)
            return "";

        return AsList(artist.City, artist.State, artist.Country);
    }

    public static string DiscPosition(this AlbumDetailsVM album, AlbumTrackLinkVM albumTrack)
    {
        var discCount = album.DiscCount == 0 ? 1 : album.DiscCount;
        return $"Disc {albumTrack.DiscNo} of {discCount}";
    }

    public static string TrackPosition(this AlbumDetailsVM album, AlbumTrackLinkVM albumTrack)
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