namespace Cadenza.Database;

public interface IPlayTrackRepository
{
    // add more criteria later
    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByAlbum(string id);
    Task<List<PlayTrack>> BetByArtist(string id);
}