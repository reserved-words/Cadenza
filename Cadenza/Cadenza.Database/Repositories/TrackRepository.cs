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
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == track.Id);

        var summary = JsonConvert.SerializeObject(track);

        if (dbTrack == null)
        {
            db.Tracks.Add(new DbTrack
            {
                Id = track.Id,
                Summary = summary
            });
        }
        else
        {
            dbTrack.Summary = summary;
        }

        await db.SaveChanges();
    }

    public async Task AddTrack(FullTrack track)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == track.Id);

        var details = JsonConvert.SerializeObject(track);

        if (dbTrack == null)
        {
            db.Tracks.Add(new DbTrack
            {
                Id = track.Id,
                Details = details
            });
        }
        else
        {
            dbTrack.Details = details;
        }

        await db.SaveChanges();
    }

    public async Task<PlayingTrack?> GetSummary(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

        if (dbTrack == null)
            return null;

        return JsonConvert.DeserializeObject<PlayingTrack>(dbTrack.Details ?? dbTrack.Summary);
    }

    public async Task<FullTrack?> GetDetails(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

        if (dbTrack?.Details == null)
            return null;

        return JsonConvert.DeserializeObject<FullTrack>(dbTrack.Details);
    }
}
