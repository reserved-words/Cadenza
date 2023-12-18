namespace Cadenza.Database.Interfaces;

public interface IPlayRepository
{
    Task<List<int>> PlayAll();
    Task<List<int>> PlayAlbum(int id);
    Task<List<int>> PlayArtist(int id);
    Task<List<int>> PlayGenre(string genre, int groupingId);
    Task<List<int>> PlayGrouping(int id);
    Task<List<int>> PlayTag(string id);
}
