namespace Cadenza.Local.API;

public class PlaylistService : IPlaylistService
{
    private readonly ILibrary _library;

    public PlaylistService(ILibrary library)
    {
        _library = library;
    }

    public async Task<ICollection<Track>> All()
    {
        return await _library.GetAllTracks();
    }
}