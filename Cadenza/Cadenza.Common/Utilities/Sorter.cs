namespace Cadenza.Common;

public class Sorter : ISorter
{
    public readonly INameComparer _comparer;

    public Sorter(INameComparer comparer)
    {
        _comparer = comparer;
    }

    public IEnumerable<AlbumInfo> SortAlbums(IEnumerable<AlbumInfo> unsorted)
    {
        var test1 = unsorted.ToList();
        var test2 = test1.OrderBy(a => _comparer.GetStandardisedName(a.ArtistName)).ToList();
        var test3 = test2.OrderBy(a => a.ReleaseType).ToList();
        var test4 = test3.OrderBy(a => a.Year).ToList();

        return unsorted
            .OrderBy(a => _comparer.GetStandardisedName(a.ArtistName))
            .ThenBy(a => a.ReleaseType)
            .ThenBy(a => a.Year);
    }

    public IEnumerable<Artist> SortArtists(IEnumerable<Artist> unsorted)
    {
        return unsorted.OrderBy(a => _comparer.GetStandardisedName(a.Name));
    }

    public IEnumerable<Disc> SortDiscs(IEnumerable<Disc> unsorted)
    {
        return unsorted.OrderBy(d => d.DiscNo);
    }

    public IEnumerable<AlbumTrack> SortTracks(IEnumerable<AlbumTrack> unsorted)
    {
        return unsorted
            .OrderBy(t => t.Position.DiscNo)
            .ThenBy(t => t.Position.TrackNo);
    }
}