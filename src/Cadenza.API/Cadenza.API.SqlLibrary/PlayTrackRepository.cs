
namespace Cadenza.Database.SqlLibrary;

internal class PlayTrackRepository : IPlayTrackRepository
{
    private readonly ILibraryReader _libraryReader;

    public PlayTrackRepository(ILibraryReader libraryReader)
    {
        _libraryReader = libraryReader;
    }

    public async Task<List<int>> PlayAlbum(int id)
    {
        return await _libraryReader.GetAbumTrackIds(id);
    }

    public async Task<List<int>> PlayAll()
    {
        return await _libraryReader.GetAllTrackIds();
    }

    public async Task<List<int>> PlayArtist(int id)
    {
        return await _libraryReader.GetArtistTrackIds(id);
    }

    public async Task<List<int>> PlayGenre(string id)
    {
        return await _libraryReader.GetGenreTrackIds(id);
    }

    public async Task<List<int>> PlayGrouping(int id)
    {
        return await _libraryReader.GetGroupingTrackIds(id);
    }

    public async Task<List<int>> PlayTag(string id)
    {
        return await _libraryReader.GetTagTrackIds(id);
    }
}
