using Cadenza.Common;
using IndexedDB.Blazor;

namespace Cadenza.Database;

public class TrackRepository : ITrackRepositoryUpdater
{
    private readonly IIndexedDbFactory _dbFactory;

    public TrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task AddTrack(TrackInfo track)
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            db.Tracks.Add(new DbTrack
            {
                Id = track.Id,
                Source = track.Source,
                Title = track.Title,
                ArtistId = track.ArtistId,
                AlbumId = track.AlbumId,
                DurationSeconds = track.DurationSeconds,
                Year = track.Year,
                Lyrics = track.Lyrics,
                // Tags
                // TrackNo
                // DiscNo
            });

            await db.SaveChanges();
        }
    }

    public async Task<PlayingTrack> GetSummary(LibrarySource source, string id)
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

            if (dbTrack == null)
                return null;

            var dbAlbum = db.Albums.Single(a => a.Id == dbTrack.AlbumId);
            var dbArtist = db.Artists.Single(a => a.Id == dbTrack.ArtistId);

            return new PlayingTrack
            {
                Id = dbTrack.Id,
                Source = dbAlbum.Source,
                DurationSeconds = dbTrack.DurationSeconds,
                Title = dbTrack.Title,
                Artist = dbArtist.Name,
                AlbumTitle = dbAlbum.Title,
                AlbumArtist = dbAlbum.ArtistName
            };
        }
    }
}
