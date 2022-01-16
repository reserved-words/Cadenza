namespace Cadenza.Library;

public class LibraryUpdater : ILibraryUpdater
{
    private readonly ICacher _cache;

    public LibraryUpdater(IMerger merger, ICache cache)
    {
        _cache = new SimpleCacher(merger, cache);
    }

    public async Task<bool> UpdateAlbum(AlbumInfo album)
    {
        _cache.AddAlbum(album, true);
        return true;
    }

    public async Task<bool> UpdateArtist(ArtistInfo artist)
    {
        _cache.AddArtist(artist, false, true);
        return true;
    }

    public async Task<bool> UpdateTrack(TrackInfo track)
    {
        _cache.AddTrack(track, true);
        return true;
    }
}