using Cadenza.Core;
using Cadenza.Domain;

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

    public async Task<PlayingTrack> GetSummary(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        return GetById(db, id);
    }

    public async Task<FullTrack> GetDetails(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

        if (dbTrack?.Details == null)
            return null;

        return JsonConvert.DeserializeObject<FullTrack>(dbTrack.Details);
    }

    public async Task AddTrack(AlbumTrackInfo track)
    {
        // Add to track summaries
        // And add to album playtracks
        // And add positions?
    }

    public async Task<List<AlbumTrackInfo>> GetAlbumTracks(LibrarySource source, string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var list = new List<AlbumTrackInfo>();

        var playTracks = db.GetPlayTracks(PlayTrackType.Album, id, source);

        foreach (var track in playTracks)
        {
            var dbTrack = db.Tracks.Single(t => t.Id == track.Id);

            list.Add(new AlbumTrackInfo
            {
                Track = GetById(db, id),
                DiscNo = 0,
                TrackNo = 0
            });
        }

        return list;
    }

    private PlayingTrack GetById(LibraryDb db, string id)
    {
        var dbTrack = db.Tracks.SingleOrDefault(t => t.Id == id);

        if (dbTrack == null)
            return null;

        return JsonConvert.DeserializeObject<PlayingTrack>(dbTrack.Details ?? dbTrack.Summary);
    }
}
