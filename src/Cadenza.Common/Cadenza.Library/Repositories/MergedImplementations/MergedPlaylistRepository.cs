namespace Cadenza.Library;

public class MergedPlaylistRepository : IMergedPlaylistRepository
{
    private readonly IEnumerable<ISourcePlaylistRepository> _sources;

    public MergedPlaylistRepository(IEnumerable<ISourcePlaylistRepository> sources)
    {
        _sources = sources;
    }


    public async Task<Playlist> GetPlaylist(LibrarySource source, string id)
    {
        var sourceRepository = _sources.Single(s => s.Source == source);
        return await sourceRepository.GetPlaylist(id);
    }

    public async Task<List<PlaylistTrack>> GetTracks(LibrarySource source, string playlistId)
    {
        var sourceRepository = _sources.Single(s => s.Source == source);
        return await sourceRepository.GetPlaylistTracks(playlistId);
    }
}
