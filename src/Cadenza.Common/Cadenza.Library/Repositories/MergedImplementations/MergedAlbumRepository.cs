namespace Cadenza.Library;

public class MergedAlbumRepository : IMergedAlbumRepository
{
    private readonly IEnumerable<ISourceAlbumRepository> _sources;

    public MergedAlbumRepository(IEnumerable<ISourceAlbumRepository> sources)
    {
        _sources = sources;
    }


    public async Task<AlbumInfo> GetAlbum(LibrarySource source, string id)
    {
        var sourceRepository = _sources.Single(s => s.Source == source);
        return await sourceRepository.GetAlbum(id);
    }

    public async Task<List<AlbumTrack>> GetTracks(LibrarySource source, string albumId)
    {
        var sourceRepository = _sources.Single(s => s.Source == source);
        return await sourceRepository.GetAlbumTracks(albumId);
    }
}
