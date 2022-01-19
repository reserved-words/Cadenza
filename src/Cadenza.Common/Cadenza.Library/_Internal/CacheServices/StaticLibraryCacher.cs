namespace Cadenza.Library;

internal class StaticLibraryCacher : IStaticLibraryCacher
{
    private readonly IMerger _merger;

    public StaticLibraryCacher(IMerger merger)
    {
        _merger = merger;
    }

    public void MergeStaticLibrary(StaticLibrary baseLibrary, StaticLibrary newLibrary, MergeMode mode)
    {
        if (newLibrary == null)
            return;

        newLibrary.Artists.ForEach(a => AddArtist(baseLibrary, a, mode));
        newLibrary.Albums.ForEach(a => AddAlbum(baseLibrary, a, mode));
        newLibrary.Tracks.ForEach(t => AddTrack(baseLibrary, t, mode));
        newLibrary.AlbumTrackLinks.ForEach(t => AddAlbumTrack(baseLibrary, t, mode));
    }

    public void AddArtist(StaticLibrary baseLibrary, ArtistInfo newItem, MergeMode mode)
    {
        var existingItem = baseLibrary.Artists.SingleOrDefault(a => a.Id == newItem.Id);
        if (existingItem == null)
        {
            baseLibrary.Artists.Add(newItem);
            return;
        }

        _merger.MergeArtist(existingItem, newItem, mode);
    }

    public void AddAlbum(StaticLibrary baseLibrary, AlbumInfo newItem, MergeMode mode)
    {
        var existingItem = baseLibrary.Albums.SingleOrDefault(a => a.Id == newItem.Id);
        if (existingItem == null)
        {
            baseLibrary.Albums.Add(newItem);
            return;
        }

        _merger.MergeAlbum(existingItem, newItem, mode);
    }

    public void AddTrack(StaticLibrary baseLibrary, TrackInfo newItem, MergeMode mode)
    {
        var existingItem = baseLibrary.Tracks.SingleOrDefault(a => a.Id == newItem.Id);
        if (existingItem == null)
        {
            baseLibrary.Tracks.Add(newItem);
            return;
        }

        _merger.MergeTrack(existingItem, newItem, mode);
    }

    public void AddAlbumTrack(StaticLibrary baseLibrary, AlbumTrackLink newItem, MergeMode mode)
    {
        var existingItem = baseLibrary.AlbumTrackLinks.SingleOrDefault(t => t.TrackId == newItem.TrackId);
        if (existingItem == null)
        {
            baseLibrary.AlbumTrackLinks.Add(newItem);
            return;
        }

        _merger.MergeAlbumTrackLink(existingItem, newItem, mode);
    }
}