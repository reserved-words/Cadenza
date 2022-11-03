//namespace Cadenza.API.Cache.Services;

//internal class CacheContainer : ICacheContainer
//{

//    public List<PlayerItem> GetItems(PlayerItemType type) => _itemCache.GetItems(type);
//    public List<PlayerItem> GetTags() => _itemCache.GetAll();
//    public List<PlayerItem> GetTag(string id) => _itemCache.Get(id);

//    public List<PlayTrack> PlayAll() => _playCache.GetAll();
//    public List<PlayTrack> PlayTag(string id) => _playCache.GetTag(id);
//    public PlayTrack PlayTrack(string id) => _playCache.GetTrack(id);

//    public AlbumInfo GetAlbum(string id) => _mainCache.GetAlbum(id);
//    public List<Album> GetAlbumsByArtist(string id) => _helperCache.GetAlbumsByArtist(id);
//    public List<AlbumTrack> GetAlbumTracks(string id) => _helperCache.GetAlbumTracks(id);
//    public List<Artist> GetAllArtists() => _mainCache.GetAllArtists();
//    public ArtistInfo GetArtist(string id) => _mainCache.GetArtist(id);
//    public List<Track> GetArtistTracks(string id) => _helperCache.GetArtistTracks(id);
//    public TrackFull GetFullTrack(string id) => _mainCache.GetFullTrack(id);
//    public TrackInfo GetTrack(string id) => _mainCache.GetTrack(id);
//    public List<Artist> GetArtistsByGenre(string id) => _helperCache.GetArtistsByGenre(id);
//    public List<Artist> GetArtistsByGrouping(Grouping id) => _helperCache.GetArtistsByGrouping(id);
//}
