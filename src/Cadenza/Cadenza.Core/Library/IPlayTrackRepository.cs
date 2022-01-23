namespace Cadenza.Core;

public interface IPlayTrackRepository
{
    Task<IEnumerable<BasicTrack>> GetAll();
    Task<IEnumerable<BasicTrack>> GetByAlbum(string id);
    Task<IEnumerable<BasicTrack>> GetByArtist(string id);
}
