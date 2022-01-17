namespace Cadenza.Library;

internal class StaticLibraryCacher : IStaticLibraryCacher
{
    private readonly ICacher _itemCacher;

    public StaticLibraryCacher(ICacher itemCacher)
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