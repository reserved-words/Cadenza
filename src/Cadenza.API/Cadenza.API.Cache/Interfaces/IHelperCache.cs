namespace Cadenza.API.Cache.Interfaces;

internal interface IHelperCache
{
    void CacheArtist(ArtistDetailsDTO album);
    void Clear();
    List<ArtistDTO> GetArtistsByGenre(string id);
    List<ArtistDTO> GetArtistsByGrouping(int id);
}
