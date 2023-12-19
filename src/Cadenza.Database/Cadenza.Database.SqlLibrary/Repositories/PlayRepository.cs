using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class PlayRepository : IPlayRepository
{
    private readonly IPlay _play;

    public PlayRepository(IPlay play)
    {
        _play = play;
    }

    public async Task<string> GetAlbumName(int id)
    {
        var album = await _play.GetAlbum(id);
        return $"{album.Title} ({album.ArtistName})";
    }

    public async Task<string> GetArtistName(int id)
    {
        var artist = await _play.GetArtist(id);
        return artist.Name;
    }

    public async Task<string> GetGenreName(string grouping, string genre)
    {
        var result = await _play.GetGenre(grouping, genre);
        return result.IsUniqueGenre
            ? result.Genre
            : $"{result.Genre} ({result.Grouping})";
    }

    public async Task<string> GetTrackName(int id)
    {
        var track = await _play.GetTrack(id);
        return $"{track.Title} ({track.ArtistName})";
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

    public async Task<List<int>> PlayGenre(string grouping, string genre)
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
