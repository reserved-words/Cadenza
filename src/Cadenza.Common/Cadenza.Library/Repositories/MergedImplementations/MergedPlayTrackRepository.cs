namespace Cadenza.Library;

public class MergedPlayTrackRepository : MergedRepositoryBase<ISourcePlayTrackRepository>, IMergedPlayTrackRepository
{
    public MergedPlayTrackRepository(IEnumerable<ISourcePlayTrackRepository> sources)
        :base(sources)
    {
    }

    protected override int ItemFetchLimit => 500;

    public async Task<List<PlayTrack>> GetAll()
    {
        return await Fetch((repository, page, limit) => repository.GetAll(page, limit));
    }

    public async Task<List<PlayTrack>> GetByAlbum(string id)
    {
        return await Fetch((repository, page, limit) => repository.GetByAlbum(id, page, limit));
    }

    public async Task<List<PlayTrack>> GetByArtist(string id)
    {
        return await Fetch((repository, page, limit) => repository.GetByArtist(id, page, limit));
    }

    public async Task<List<PlayTrack>> GetByGenre(string id)
    {
        return await Fetch((repository, page, limit) => repository.GetByGenre(id, page, limit));
    }

    public async Task<List<PlayTrack>> GetByGrouping(Grouping id)
    {
        return await Fetch((repository, page, limit) => repository.GetByGrouping(id, page, limit));
    }
}
