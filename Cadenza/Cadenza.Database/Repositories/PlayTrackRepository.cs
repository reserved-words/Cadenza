namespace Cadenza.Database;

public class PlayTrackRepository : IPlayTrackRepository
{
    public Task<List<PlayTrack>> BetByArtist(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PlayTrack>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<PlayTrack>> GetByAlbum(string id)
    {
        throw new NotImplementedException();
    }
}
