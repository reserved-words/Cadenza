namespace Cadenza.Common;

public interface ISorter
{
    IEnumerable<Artist> SortArtists(IEnumerable<Artist> unsorted);
    IEnumerable<AlbumInfo> SortAlbums(IEnumerable<AlbumInfo> unsorted);
    IEnumerable<Disc> SortDiscs(IEnumerable<Disc> unsorted);
    IEnumerable<AlbumTrack> SortTracks(IEnumerable<AlbumTrack> unsorted);
}
