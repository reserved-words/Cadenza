using Cadenza.Core;
using Cadenza.Domain;

namespace Cadenza.Database;

public class PlayTrackRepository : IPlayTrackRepositoryUpdater
{
    private readonly IIndexedDbFactory _dbFactory;

    public PlayTrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<PlayTrack>> GetByArtist(string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return GetAllSources(db, PlayTrackType.Artist, id);
    }

    public async Task<IEnumerable<PlayTrack>> GetAll()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return GetAllSources(db, PlayTrackType.All, "");
    }

    public async Task<IEnumerable<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return db.GetPlayTracks(PlayTrackType.Album, albumId, source);
    }

    public async Task AddAlbumTracks(LibrarySource source, string albumId, IEnumerable<string> tracks)
    {
        await AddTracks(PlayTrackType.Album, albumId, source, tracks);
    }

    public async Task AddArtistTracks(LibrarySource source, string artistId, IEnumerable<string> tracks)
    {
        await AddTracks(PlayTrackType.Artist, artistId, source, tracks);
    }

    public async Task AddAllTracks(LibrarySource source, IEnumerable<string> tracks)
    {
        await AddTracks(PlayTrackType.All, "", source, tracks);
    }

    private async Task AddTracks(PlayTrackType type, string id, LibrarySource source, IEnumerable<string> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        db.PlayTracks.Add(new DbPlayTracks
        {
            Id = DbHelper.GetId(type, id, source),
            Tracks = string.Join(",", tracks)
        });

        await db.SaveChanges();
    }

    private static List<PlayTrack> GetAllSources(LibraryDb db, PlayTrackType type, string id)
    {
        return Enum.GetValues<LibrarySource>()
            .SelectMany(s => db.GetPlayTracks(type, id, s))
            .ToList();
    }
}
