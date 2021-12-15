using Cadenza.Common;
using IndexedDB.Blazor;

namespace Cadenza.Database;

public class PlayTrackRepository : IPlayTrackRepositoryUpdater
{
    private readonly IIndexedDbFactory _dbFactory;

    public PlayTrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<PlayTrack>> GetByArtist(string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return GetAllSources(db, PlayTrackType.Artist, id);
    }

    public async Task<List<PlayTrack>> GetAll()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        return GetAllSources(db, PlayTrackType.All, "");
    }

    public async Task<List<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        var id = GetId(PlayTrackType.Album, albumId, source);
        return GetById(db, id, source);
    }

    public async Task AddAlbumTracks(LibrarySource source, string albumId, List<string> tracks)
    {
        await AddTracks(PlayTrackType.Album, albumId, source, tracks);
    }

    public async Task AddArtistTracks(LibrarySource source, string artistId, List<string> tracks)
    {
        await AddTracks(PlayTrackType.Artist, artistId, source, tracks);
    }

    public async Task AddAllTracks(LibrarySource source, List<string> tracks)
    {
        await AddTracks(PlayTrackType.All, "", source, tracks);
    }

    private async Task AddTracks(PlayTrackType type, string id, LibrarySource source, List<string> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        db.PlayTracks.Add(new DbPlayTracks
        {
            Id = GetId(type, id, source),
            Tracks = string.Join(",", tracks)
        });

        await db.SaveChanges();
    }

    private static List<PlayTrack> GetAllSources(LibraryDb db, PlayTrackType type, string id)
    {
        return Enum.GetValues<LibrarySource>()
            .SelectMany(s => GetById(db, GetId(type, id, s), s))
            .ToList();
    }

    private static List<PlayTrack> GetById(LibraryDb db, string id, LibrarySource source)
    {
        var tracks = db.PlayTracks
            .SingleOrDefault(a => a.Id == id);

        if (tracks == null)
            return new List<PlayTrack>();

        return tracks.Tracks
            .Split(",")
            .Select(t => new PlayTrack
            {
                Id = t,
                Source = source
            })
            .ToList();
    }

    private static string GetId(PlayTrackType type, string id, LibrarySource source)
    {
        return $"{type}|{id}|{source}";
    }
}
