namespace Cadenza.Library;

public class LibraryUpdater : ILibraryUpdater
{
    private readonly ISimpleCacher _cache;

    public LibraryUpdater(IMerger merger, ICache cache)
    {
        _cache = new SimpleCacher(merger, cache);
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        _cache.AddAlbum(album.Item, true);
        return true;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        _cache.AddArtist(artist.Item, false, true);
        return true;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        _cache.AddTrack(track.Item, true);
        return true;
    }
}