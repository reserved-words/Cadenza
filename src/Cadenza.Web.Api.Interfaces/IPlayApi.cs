namespace Cadenza.Web.Api.Interfaces;

public interface IPlayApi
{
    Task<List<int>> PlayAll();
    Task<List<int>> PlayAlbum(int id);
    Task<List<int>> PlayArtist(int id);
    Task<List<int>> PlayGenre(string id);
    Task<List<int>> PlayGrouping(int id);
    Task<List<int>> PlayTag(string id);
}
