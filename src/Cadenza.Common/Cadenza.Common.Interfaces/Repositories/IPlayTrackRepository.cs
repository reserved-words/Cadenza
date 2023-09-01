using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IPlayTrackRepository
{
    Task<List<PlayTrack>> PlayAll();
    Task<List<PlayTrack>> PlayAlbum(int id);
    Task<List<PlayTrack>> PlayArtist(int id);
    Task<List<PlayTrack>> PlayGenre(string id);
    Task<List<PlayTrack>> PlayGrouping(int id);
    Task<List<PlayTrack>> PlayTag(string id);
}
