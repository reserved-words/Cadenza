namespace Cadenza.Library;

public class MergedArtistRepository : MergedRepositoryBase<ISourceArtistRepository>, IMergedArtistRepository
{
    private readonly IMerger _merger;

    public MergedArtistRepository(IEnumerable<ISourceArtistRepository> sources, IMerger merger)
        : base(sources)
    {
        _merger = merger;
    }

    protected override int ItemFetchLimit => 500;

    public async Task<List<Artist>> GetAlbumArtists()
    {
        return await Fetch((repository, page, limit) => repository.GetAlbumArtists(page, limit));
    }

    public async Task<List<Album>> GetArtistAlbums(string id)
    {
        return await Fetch((repository, page, limit) => repository.GetAlbums(id, page, limit));
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        return await Fetch((repository, page, limit) => repository.GetAllArtists(page, limit));
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        ArtistInfo artist = null;

        foreach (var source in Sources)
        {
            if (artist == null)
            {
                artist = await source.GetArtist(id);
            }
            else
            {
                var update = await source.GetArtist(id);
                
                if (update != null)
                {
                    _merger.MergeArtist(artist, update, MergeMode.Merge);
                }
            }
        }

        return artist;
    }

    public async Task<List<Artist>> GetTrackArtists()
    {
        return await Fetch((repository, page, limit) => repository.GetTrackArtists(page, limit));
    }

    public async Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        return await FetchArtists((repository, page, limit) => repository.GetArtistsByGrouping(id, page, limit));
    }

    public async Task<List<Artist>> GetArtistsByGenre(string id)
    {
        return await FetchArtists((repository, page, limit) => repository.GetArtistsByGenre(id, page, limit));
    }


    public async Task UpdateArtist(ArtistUpdate update)
    {
        foreach (var source in Sources)
        {
            await source.UpdateArtist(update);
        }
    }
}
