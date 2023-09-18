using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IPlayTrackRepository
{
    Task<List<int>> PlayAll();
    Task<List<int>> PlayAlbum(int id);
    Task<List<int>> PlayArtist(int id);
    Task<List<int>> PlayGenre(string id);
    Task<List<int>> PlayGrouping(int id);
    Task<List<int>> PlayTag(string id);
}
