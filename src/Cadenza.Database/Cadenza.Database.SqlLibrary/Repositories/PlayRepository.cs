using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class PlayRepository : IPlayRepository
{
    private readonly IPlay _play;

    public PlayRepository(IPlay play)
    {
        _play = play;
    }

    public async Task<List<int>> PlayAlbum(int id)
    {
        return await _play.GetAlbumTrackIds(id);
    }

    public async Task<List<int>> PlayAll()
    {
        return await _play.GetAllTrackIds();
    }

    public async Task<List<int>> PlayArtist(int id)
    {
        return await _play.GetArtistTrackIds(id);
    }

    public async Task<List<int>> PlayGenre(string genre, string grouping)
    {
        return await _play.GetGenreTrackIds(genre, grouping);
    }

    public async Task<List<int>> PlayGrouping(string grouping)
    {
        return await _play.GetGroupingTrackIds(grouping);
    }

    public async Task<List<int>> PlayTag(string id)
    {
        return await _play.GetTagTrackIds(id);
    }
}
