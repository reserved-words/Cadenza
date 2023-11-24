namespace Cadenza.API.Cache.Services;

internal class HelperCache : IHelperCache
{
    private readonly Dictionary<int, List<AlbumDetailsDTO>> _albumsByArtist = new();
    private readonly Dictionary<int, List<AlbumDetailsDTO>> _albumsFeaturingArtist = new();

    private readonly Dictionary<string, List<ArtistDetailsDTO>> _artistsByGenre = new();
    private readonly Dictionary<int, List<ArtistDetailsDTO>> _artistsByGrouping = new();

    public void CacheAlbum(AlbumDetailsDTO album)
    {
        _albumsByArtist.Cache(album.ArtistId, album);
    }

    public void CacheAlbumFeaturingArtist(int artistId, AlbumDetailsDTO album)
    {
        _albumsFeaturingArtist.Cache(artistId, album);
    }

    public void CacheArtist(ArtistDetailsDTO artist)
    {
        _artistsByGrouping.Cache(artist.Grouping.Id, artist);
        _artistsByGenre.Cache(artist.Genre, artist);
    }

    public void Clear()
    {
        _albumsByArtist.Clear();
        _albumsFeaturingArtist.Clear();
        _artistsByGenre.Clear();
        _artistsByGrouping.Clear();
    }

    public List<AlbumDTO> GetAlbumsByArtist(int id)
    {
        return _albumsByArtist.GetList<int, AlbumDetailsDTO, AlbumDTO>(id);
    }

    public List<AlbumDTO> GetAlbumsFeaturingArtist(int id)
    {
        return _albumsFeaturingArtist.GetList<int, AlbumDetailsDTO, AlbumDTO>(id);
    }

    public List<ArtistDTO> GetArtistsByGenre(string id)
    {
        return _artistsByGenre.GetList<string, ArtistDetailsDTO, ArtistDTO>(id);
    }

    public List<ArtistDTO> GetArtistsByGrouping(int id)
    {
        return _artistsByGrouping.GetList<int, ArtistDetailsDTO, ArtistDTO>(id);
    }
}
