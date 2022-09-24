using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;

namespace Cadenza.Library;

public interface IPlayTrackRepository
{
    Task<List<PlayTrack>> GetByAlbum(string id);

    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByArtist(string id);
    Task<List<PlayTrack>> GetByGenre(string id);
    Task<List<PlayTrack>> GetByGrouping(Grouping id);
}
