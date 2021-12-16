using Cadenza.Common;
using IndexedDB.Blazor;
using Newtonsoft.Json;

namespace Cadenza.Database;

public class TrackRepository : ITrackRepositoryUpdater
{
    private readonly IIndexedDbFactory _dbFactory;

    public TrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task AddTrack(PlayingTrack track)
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            db.Tracks.Add(new DbTrack
            {
                Id = track.Id,
                Details = JsonConvert.SerializeObject(track)
            });

            await db.SaveChanges();
        }
    }

    public async Task<PlayingTrack> GetSummary(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

        if (dbTrack == null)
            return null;

        return JsonConvert.DeserializeObject<PlayingTrack>(dbTrack.Details);
    }
}
