namespace Cadenza.API.Cache.Services;

internal class HelperCache : IHelperCache
{
    private readonly Dictionary<string, List<ArtistDetailsDTO>> _artistsByGenre = new();
    private readonly Dictionary<int, List<ArtistDetailsDTO>> _artistsByGrouping = new();

    public void CacheArtist(ArtistDetailsDTO artist)
    {
        _artistsByGrouping.Cache(artist.Grouping.Id, artist);
        _artistsByGenre.Cache(artist.Genre, artist);
    }

    public void Clear()
    {
        _artistsByGenre.Clear();
        _artistsByGrouping.Clear();
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
