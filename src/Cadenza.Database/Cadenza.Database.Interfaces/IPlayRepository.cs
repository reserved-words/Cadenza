namespace Cadenza.Database.Interfaces;

public interface IPlayRepository
{
    Task<string> GetArtistName(int id);
    Task<string> GetAlbumName(int id);
    Task<string> GetGenreName(string grouping, string genre);
    Task<string> GetTrackName(int id);

    Task<List<int>> PlayAll();
    Task<List<int>> PlayAlbum(int id);
    Task<List<int>> PlayArtist(int id);
    Task<List<int>> PlayGenre(string grouping, string genre);
    Task<List<int>> PlayGrouping(string grouping);
    Task<List<int>> PlayTag(string id);
}
