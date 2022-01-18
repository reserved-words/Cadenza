namespace Cadenza.Library;

internal class StaticLibraryCacher : IStaticLibraryCacher
{
    private readonly IMerger _merger;

    public StaticLibraryCacher(IMerger merger)
    {
        _merger = merger;
    }

    public void AddStaticLibrary(StaticLibrary baseLibrary, StaticLibrary newLibrary, bool forceUpdate)
    {
        if (newLibrary == null)
            return;

        newLibrary.Artists.ForEach(a => AddArtist(baseLibrary, a, forceUpdate));
        newLibrary.Albums.ForEach(a => AddAlbum(baseLibrary, a, forceUpdate));
        newLibrary.Tracks.ForEach(t => AddTrack(baseLibrary, t, forceUpdate));
        newLibrary.AlbumTrackLinks.ForEach(t => AddAlbumTrack(baseLibrary, t, forceUpdate));
    }

    public void AddArtist(StaticLibrary baseLibrary, ArtistInfo newItem, bool forceUpdate)
    {
        try
        {
            var existingItem = baseLibrary.Artists.SingleOrDefault(a => a.Id == newItem.Id);
            if (existingItem == null)
            {
                baseLibrary.Artists.Add(newItem);
                return;
            }

            _merger.MergeArtist(existingItem, newItem, forceUpdate);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public void AddAlbum(StaticLibrary baseLibrary, AlbumInfo newItem, bool forceUpdate)
    {
        var existingItem = baseLibrary.Albums.SingleOrDefault(a => a.Id == newItem.Id);
        if (existingItem == null)
        {
            baseLibrary.Albums.Add(newItem);
            return;
        }

        _merger.MergeAlbum(existingItem, newItem, forceUpdate);
    }

    public void AddTrack(StaticLibrary baseLibrary, TrackInfo newItem, bool forceUpdate)
    {
        var existingItem = baseLibrary.Tracks.SingleOrDefault(a => a.Id == newItem.Id);
        if (existingItem == null)
        {
            baseLibrary.Tracks.Add(newItem);
            return;
        }

        _merger.MergeTrack(existingItem, newItem, forceUpdate);
    }

    public void AddAlbumTrack(StaticLibrary baseLibrary, AlbumTrackLink newItem, bool forceUpdate)
    {
        var existingItem = baseLibrary.AlbumTrackLinks.SingleOrDefault(t => t.TrackId == newItem.TrackId);
        if (existingItem == null)
        {
            baseLibrary.AlbumTrackLinks.Add(newItem);
            return;
        }

        _merger.MergeAlbumTrackLink(existingItem, newItem, forceUpdate);
    }
}