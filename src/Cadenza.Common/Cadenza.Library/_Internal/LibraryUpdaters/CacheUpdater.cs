namespace Cadenza.Library;

internal class CacheUpdater : IUpdater
{
    private readonly ICacher _cache;

    public CacheUpdater(IMerger merger, ICache cache)
    {
        _cache = new SimpleCacher(merger, cache);
    }

    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        _cache.AddAlbum(album, true);
        return true;
    }

    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        _cache.AddArtist(artist, false, true);
        return true;
    }

    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        _cache.AddTrack(track, true);
        return true;
    }
}