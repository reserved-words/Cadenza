using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IPlayTrackRepository
{
    Task<List<PlayTrack>> GetByAlbum(string id);

    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByArtist(string id);
    Task<List<PlayTrack>> GetByGenre(string id);
    Task<List<PlayTrack>> GetByGrouping(Grouping id);
}
