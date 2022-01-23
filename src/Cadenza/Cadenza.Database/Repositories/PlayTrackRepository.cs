using Cadenza.Core;
using Cadenza.Domain;

namespace Cadenza.Database;

public class PlayTrackRepository : IPlayTrackRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public PlayTrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<BasicTrack>> GetByArtist(string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return GetAll(db, PlayTrackType.Artist, id);
    }

    public async Task<IEnumerable<BasicTrack>> GetAll()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return Enum.GetValues<LibrarySource>()
            .SelectMany(s => GetAll(db, PlayTrackType.All, s.ToString()));
    }

    public async Task<IEnumerable<BasicTrack>> GetByAlbum(string albumId)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return db.GetPlayTracks(PlayTrackType.Album, albumId);
    }

    private static List<BasicTrack> GetAll(LibraryDb db, PlayTrackType type, string id)
    {
        return db.GetPlayTracks(type, id);
    }
}
