namespace Cadenza.Library;

public class StaticLibraryCacher : IStaticLibraryCacher
{
    private readonly ISimpleCacher _itemCacher;

    public StaticLibraryCacher(ISimpleCacher itemCacher)
    {
        _itemCacher = itemCacher;
    }

    public void AddStaticLibrary(StaticLibrary library, bool forceUpdate)
    {
        if (library == null)
            return;

        library.Artists.ForEach(a => _itemCacher.AddArtist(a, false, false));
        library.Albums.ForEach(a => _itemCacher.AddAlbum(a, false));
        library.Tracks.ForEach(t => _itemCacher.AddTrack(t, false));
        library.AlbumTrackLinks.ForEach(_itemCacher.AddAlbumTrack);
    }
}