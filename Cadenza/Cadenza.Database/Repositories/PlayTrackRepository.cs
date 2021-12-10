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

        return db.ArtistTracks
            .Where(a => a.ArtistId == id)
            .OfType<PlayTrack>()
            .ToList();
    }

    public async Task<List<PlayTrack>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<List<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        return db.AlbumTracks
            .Where(a => a.AlbumId == albumId)
            .OfType<PlayTrack>()
            .ToList();
    }

    public async Task AddAlbumTracks(LibrarySource source, string albumId, List<string> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        foreach (var trackId in tracks)
        {
            db.AlbumTracks.Add(new DbAlbumTrack
            {
                AlbumId = albumId,
                Id = trackId,
                Source = source
            });
        }

        await db.SaveChanges();
    }

    public async Task AddArtistTracks(LibrarySource source, string artistId, List<string> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        foreach (var trackId in tracks)
        {
            db.ArtistTracks.Add(new DbArtistTrack
            {
                ArtistId = artistId,
                Source = source,
                Id = trackId
            });
        }

        await db.SaveChanges();
    }
}
